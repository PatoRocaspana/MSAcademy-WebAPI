using RentACarWebAPI.Interfaces.Repositories;
using RentACarWebAPI.Interfaces.Services;
using RentACarWebAPI.Models.Base;
using System.Collections.Generic;

namespace RentACarWebAPI.Services
{
    public class BaseService<T> : IBaseService<T> where T : Entity
    {
        private readonly IBaseRepository<T> _entityRepository;

        public BaseService(IBaseRepository<T> entityRepository)
        {
            _entityRepository = entityRepository;
        }

        public virtual T Create(T entity)
        {
            var entityCreated = _entityRepository.Create(entity);
            return entityCreated;
        }

        public virtual void Delete(int id)
        {
            _entityRepository.Delete(id);
        }

        public virtual T Get(int id)
        {
            var entity = _entityRepository.Get(id);
            return entity;
        }

        public virtual List<T> GetAll()
        {
            var entityList = _entityRepository.GetAll();
            return entityList;
        }

        public virtual T Update(T entity, int id)
        {
            var updatedEntity = _entityRepository.Update(entity, id);
            return updatedEntity;
        }

        public bool EntityExist(int id)
        {
            var entityExist = _entityRepository.EntityExist(id);
            return entityExist;
        }
    }
}
