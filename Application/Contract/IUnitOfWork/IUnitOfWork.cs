using Application.Contract.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Contract.IUnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {

        ITodoRepository Todos { get; }
        IUserRepository Users { get; }
      

        Task<int> SaveChangesAsync();
    }
}
