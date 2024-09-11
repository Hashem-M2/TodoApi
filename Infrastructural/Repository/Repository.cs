using Application.Contract.IRepository;
using Context;
using Microsoft.EntityFrameworkCore;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructural.Repository
{
    public class Repository<TEntity, TId> : IRepository<TEntity, TId> where TEntity :class
    {
        private readonly ApplicationDbContext _Context;
        private readonly DbSet<TEntity> _Dbset;
        public Repository(ApplicationDbContext Context)
        {
            _Context = Context;
            _Dbset = _Context.Set<TEntity>();
        }
      
        public async Task<TEntity> CreateAsync(TEntity entity)
        {
         
            var result = (await _Dbset.AddAsync(entity)).Entity;
            return result;
        }

        public Task<TEntity> DeleteAsync(TEntity entity)
        {
            return Task.FromResult(_Dbset.Remove(entity).Entity);
        }

        public Task<IQueryable<TEntity>> GetAllAsync()
        {
            return Task.FromResult(_Dbset.Select(s => s).AsNoTracking());
        }

        public async Task<TEntity> GetByIdAsync(TId id)
        {
            TEntity item = await _Dbset.FindAsync(id);
            return item;
        }
        public Task<TEntity> UpdateAsync(TEntity entity)
        {

            return Task.FromResult(_Dbset.Update(entity).Entity);

        }
    }

   
}
