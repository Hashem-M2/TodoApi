using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Contract.IRepository
{
    public interface ITodoRepository:IRepository<TodoItem,int>
    {

        public Task<IEnumerable<TodoItem>> GetTodosAsync(int pageNumber, int pageSize);

    }
}
