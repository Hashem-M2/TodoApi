using Application.Contract.IUnitOfWork;
using DTOS;
using DTOS.TodoDto;
using Models;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.Routing.Constraints;
using Microsoft.EntityFrameworkCore;

namespace Application.Service
{
    public class TodoService : ITodoService 
    {
        private readonly HttpClient _httpClient;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
      

        public TodoService(IUnitOfWork unitOfWork,
            IMapper mapper,
            HttpClient httpclient
            )
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _httpClient = httpclient;
            
        }
        public async Task<TodoCreateDto> CreateTodoAsync(TodoCreateDto todoCreateDto, string userId)
        {
            var existingTodo = (await _unitOfWork.Todos.GetAllAsync())
                                .Where(t => t.Title == todoCreateDto.Title && t.UserId == userId)
                                .FirstOrDefault();
            if (existingTodo != null)
            {
                throw new InvalidOperationException("Todo with this title already exists.");
            }
            var newTodo = _mapper.Map<TodoItem>(todoCreateDto);
            newTodo.UserId = userId;  
            var createdTodo = await _unitOfWork.Todos.CreateAsync(newTodo);
            await _unitOfWork.SaveChangesAsync();
            return _mapper.Map<TodoCreateDto>(createdTodo);
        }

        public async Task<TodoUpdateDto> UpdateTodoAsync(TodoUpdateDto todoUpdateDto)
        {
            var existingTodo = await _unitOfWork.Todos.GetByIdAsync(todoUpdateDto.Id);
            if (existingTodo == null)
            {
                throw new ArgumentException($"This Todo does not exist.");
            }
            var updatedTodo = _mapper.Map(todoUpdateDto, existingTodo);
            await _unitOfWork.SaveChangesAsync();   
            return _mapper.Map<TodoUpdateDto>(updatedTodo);
        }


        public async Task<TodoDto> DeleteTodoAsync(int id)
        {
            var todoToDelete = await _unitOfWork.Todos.GetByIdAsync(id);
            if (todoToDelete == null)
            {
                throw new ArgumentException($"This Todo does not exist.");
            }

            var deletedTodoDto = _mapper.Map<TodoDto>(todoToDelete);
            await _unitOfWork.Todos.DeleteAsync(todoToDelete);
            await _unitOfWork.SaveChangesAsync();
            return deletedTodoDto;
        }


        public async Task<TodoDto> GetTodoByIdAsync(int id)
        {
            var todoItem = await _unitOfWork.Todos.GetByIdAsync(id);

            if (todoItem == null)
            {
                throw new KeyNotFoundException($"Todo item was not found.");
            }

            var todoDto = _mapper.Map<TodoDto>(todoItem);
            return todoDto;
        }

        public async Task<List<TodoDto>> GetAllToDosAsync(int pageNumber, int pageSize)
        {
            var todosQuery = await _unitOfWork.Todos.GetAllAsync();
            var todosDtoQuery = _mapper.ProjectTo<TodoDto>(todosQuery);
            var pagedTodosDto = await todosDtoQuery
                                    .Skip((pageNumber - 1) * pageSize)
                                    .Take(pageSize)
                                    .ToListAsync();  

            return pagedTodosDto;
        }



        // Specialize With LiveTodo
        public async Task<IEnumerable<TodoDto>> GetTodosFromApiAsync(int pageNumber, int pageSize)
        {
            var response = await _httpClient.GetAsync("https://jsonplaceholder.typicode.com/todos");
            response.EnsureSuccessStatusCode(); 

            var jsonString = await response.Content.ReadAsStringAsync();
            var todos = JsonConvert.DeserializeObject<IEnumerable<TodoDto>>(jsonString);

            return todos
                .Skip((pageNumber - 1) * pageSize)  
                .Take(pageSize)                     
                .ToList();
        }
    }
}
