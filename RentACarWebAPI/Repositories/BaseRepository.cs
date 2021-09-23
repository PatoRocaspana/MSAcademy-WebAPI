using RentACarWebAPI.Interfaces.Repositories;
using RentACarWebAPI.Models.Base;
using System.Collections.Generic;
using System.Linq;

namespace RentACarWebAPI.Repositories
{
    public abstract class BaseRepository<T> : IBaseRepository<T> where T : Entity
    {
        private readonly RentACarDbContext _dbContext;

        public BaseRepository(RentACarDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public virtual T Create(T entity)
        {
            _dbContext.Set<T>().Add(entity);
            _dbContext.SaveChanges();

            var entityCreated = Get(entity.Id);

            return entityCreated;
        }

        public virtual T Update(T entity, int id)
        {
            var existingEntity = Get(id);

            if (existingEntity is null)
                return null;

            UpdateEntity(existingEntity, entity);

            _dbContext.SaveChanges();

            return existingEntity;
        }

        protected abstract void UpdateEntity(T existingEntity, T newEntity);

        public virtual T Get(int id)
        {
            var entity = _dbContext.Set<T>().Find(id);
            return entity;
        }

        public virtual void Delete(int id)
        {
            _dbContext.Set<T>().Remove(Get(id));
            _dbContext.SaveChanges();
        }

        public virtual List<T> GetAll()
        {
            var entityList = _dbContext.Set<T>().ToList();
            return entityList;
        }
    }
}
