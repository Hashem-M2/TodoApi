using DTOS.TodoDto;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Service
{
    public interface ITodoService
    { 
        Task<TodoDto> GetTodoByIdAsync(int id);
        Task<TodoCreateDto> CreateTodoAsync( TodoCreateDto createtodo,string userid);
        Task<TodoUpdateDto> UpdateTodoAsync(TodoUpdateDto todo);
        Task<TodoDto> DeleteTodoAsync(int Id);
        Task<List<TodoDto>> GetAllToDosAsync(int pageNumber, int pageSize);


        // Specialize With LiveTodo
        Task<IEnumerable<TodoDto>> GetTodosFromApiAsync(int pageNumber, int pageSize);
    }
}
