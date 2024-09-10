using Application.Contract.IRepository;
using Context;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructural.Repository
{
    public class UserRepository :IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<User> GetByIdAsync(string userId)
        {
            return await _context.Users.FindAsync(userId);
        }

    }
}
