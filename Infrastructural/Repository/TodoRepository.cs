using Application.Contract.IRepository;
using Context;
using DTOS.TodoDto;
using Microsoft.EntityFrameworkCore;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructural.Repository
{
    public class TodoRepository : Repository<TodoItem, int>, ITodoRepository
    {
        private readonly ApplicationDbContext _context;

        public TodoRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TodoItem>> GetTodosAsync(int pageNumber, int pageSize)
        {
            return await _context.TodoItems
                   .Skip((pageNumber - 1) * pageSize) 
                   .Take(pageSize)                   
                   .Include(t => t.User)            
                   .ToListAsync();
        } 






    }
}
