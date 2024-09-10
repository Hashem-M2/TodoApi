using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Contract.IRepository
{
    public interface IUserRepository
    {
        public Task<User> GetByIdAsync(string userId);

    }
}
