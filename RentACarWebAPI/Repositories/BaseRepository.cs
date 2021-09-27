using Microsoft.EntityFrameworkCore;
using RentACarWebAPI.Interfaces.Repositories;
using RentACarWebAPI.Models.Base;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RentACarWebAPI.Repositories
{
    public abstract class BaseRepository<T> : IBaseRepository<T> where T : Entity
    {
        protected readonly RentACarDbContext DbContext;

        public BaseRepository(RentACarDbContext dbContext)
        {
            DbContext = dbContext;
        }

        public virtual async Task<T> CreateAsync(T entity)
        {
            await DbContext.Set<T>().AddAsync(entity);
            await DbContext.SaveChangesAsync();

            return entity;
        }

        public virtual async Task<T> UpdateAsync(T entity, int id)
        {
            var existingEntity = await GetAsync(id);

            if (existingEntity is null)
                return null;

            UpdateEntity(existingEntity, entity);

            await DbContext.SaveChangesAsync();

            return existingEntity;
        }

        protected abstract void UpdateEntity(T existingEntity, T newEntity);

        public virtual async Task<T> GetAsync(int id)
        {
            var entity = await DbContext.Set<T>().FindAsync(id);
            return entity;
        }

        public virtual async Task DeleteAsync(int id)
        {
            var entityToDelete = await GetAsync(id);
            DbContext.Set<T>().Remove(entityToDelete);
            await DbContext.SaveChangesAsync();
        }

        public virtual async Task<List<T>> GetAllAsync()
        {
            var entityList = await DbContext.Set<T>().ToListAsync();
            return entityList;
        }

        public virtual async Task<bool> EntityExistAsync(int id)
        {
            var entityExists = await DbContext.Set<T>().AnyAsync(e => e.Id == id);
            return entityExists;
        }
    }
}
