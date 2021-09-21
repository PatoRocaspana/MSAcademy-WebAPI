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

        public T Create(T entity)
        {
            var entityCreated = _entityRepository.Create(entity);
            return entityCreated;
        }

        public void Delete(int id)
        {
            _entityRepository.Delete(id);
        }

        public T Get(int id)
        {
            var entity = _entityRepository.Get(id);
            return entity;
        }

        public List<T> GetAll()
        {
            var entityList = _entityRepository.GetAll();
            return entityList;
        }

        public T Update(T entity, int id)
        {
            var updatedEntity = _entityRepository.Update(entity, id);
            return updatedEntity;
        }
    }
}
