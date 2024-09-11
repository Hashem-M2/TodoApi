using Application.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
   [AllowAnonymous]
    public class LiveTodoController : ControllerBase
    {
        private readonly ITodoService _toDoService;

        public LiveTodoController(ITodoService toDoService)
        {
            _toDoService = toDoService;
        }


        [HttpGet("Read")]
        public async Task<IActionResult> GetTodos([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            if (pageNumber <= 0 || pageSize <= 0)
            {
                return BadRequest("Page number and page size must be greater than zero.");
            }

            var todos = await _toDoService.GetTodosFromApiAsync(pageNumber, pageSize);

            var totalRecords = 200; 
            var totalPages = (int)Math.Ceiling((double)totalRecords / pageSize);

            var response = new
            {
                Data = todos,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalPages = totalPages,
                TotalRecords = totalRecords
            };

            return Ok(response);
        }
    }
}
