using Application.Service;
using DTOS.TodoDto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class TodoController : ControllerBase
    {
        private readonly ITodoService _todoService;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public TodoController(ITodoService todoService,UserManager<User> usermanager,
            SignInManager<User> signmanager)
        {
            _todoService = todoService;
            _userManager = usermanager;
            _signInManager = signmanager;

        }
        [HttpPost]

        [HttpPost]
        public async Task<IActionResult> CreateTodo([FromBody] TodoCreateDto todoDto)
        {

            var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            var createdTodo = await _todoService.CreateTodoAsync(todoDto, userId);
            if (createdTodo == null)
            {
                return BadRequest("Failed to create todo item.");
            }

            return Ok(createdTodo);
        }











        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTodo(int id, [FromBody] TodoUpdateDto todoUpdateDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId == null)
            {
                return Unauthorized("User is not authenticated.");
            }

            var result = await _todoService.UpdateTodoAsync(todoUpdateDto);

            if (result == null)
            {
                return NotFound("Todo item not found.");
            }

            return Ok(result);
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodo(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId == null)
            {
                return Unauthorized("User is not authenticated.");
            }

            var deletedTodo = await _todoService.DeleteTodoAsync(id);

            if (deletedTodo == null)
            {
                return NotFound("Todo item not found.");
            }

            return Ok(deletedTodo);
        }

        [HttpGet]
        public async Task<IActionResult> GetTodos(int pageNumber = 1, int pageSize = 10)
        {
            var todos = await _todoService.GetAllToDosAsync(pageNumber, pageSize);

            if (todos == null || !todos.Any())
            {
                return NotFound("No todos found.");
            }

            return Ok(todos);
        }


    }
}
