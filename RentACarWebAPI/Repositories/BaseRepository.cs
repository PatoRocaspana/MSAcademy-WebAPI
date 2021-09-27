using RentACarWebAPI.Interfaces.Repositories;
using RentACarWebAPI.Models.Base;
using System.Collections.Generic;
using System.Linq;

namespace RentACarWebAPI.Repositories
{
    public abstract class BaseRepository<T> : IBaseRepository<T> where T : Entity
    {
        protected readonly RentACarDbContext DbContext;

        public BaseRepository(RentACarDbContext dbContext)
        {
            DbContext = dbContext;
        }

        public virtual T Create(T entity)
        {
            DbContext.Set<T>().Add(entity);
            DbContext.SaveChanges();

            return entity;
        }

        public virtual T Update(T entity, int id)
        {
            var existingEntity = Get(id);

            if (existingEntity is null)
                return null;

            UpdateEntity(existingEntity, entity);

            DbContext.SaveChanges();

            return existingEntity;
        }

        protected abstract void UpdateEntity(T existingEntity, T newEntity);

        public virtual T Get(int id)
        {
            var entity = DbContext.Set<T>().Find(id);
            return entity;
        }

        public virtual void Delete(int id)
        {
            DbContext.Set<T>().Remove(Get(id));
            DbContext.SaveChanges();
        }

        public virtual List<T> GetAll()
        {
            var entityList = DbContext.Set<T>().ToList();
            return entityList;
        }

        public virtual bool EntityExist(int id)
        {
            var entityExists = DbContext.Set<T>().Any(e => e.Id == id);
            return entityExists;
        }
    }
}
