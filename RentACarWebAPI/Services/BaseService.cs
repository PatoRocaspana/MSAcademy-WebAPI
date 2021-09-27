using RentACarWebAPI.Interfaces.Repositories;
using RentACarWebAPI.Interfaces.Services;
using RentACarWebAPI.Models.Base;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RentACarWebAPI.Services
{
    public class BaseService<T> : IBaseService<T> where T : Entity
    {
        private readonly IBaseRepository<T> _entityRepository;

        public BaseService(IBaseRepository<T> entityRepository)
        {
            _entityRepository = entityRepository;
        }

        public virtual async Task<T> CreateAsync(T entity)
        {
            var entityCreated = await _entityRepository.CreateAsync(entity);
            return entityCreated;
        }

        public virtual async Task DeleteAsync(int id)
        {
            await _entityRepository.DeleteAsync(id);
        }

        public virtual async Task<T> GetAsync(int id)
        {
            var entity = await _entityRepository.GetAsync(id);
            return entity;
        }

        public virtual async Task<List<T>> GetAllAsync()
        {
            var entityList = await _entityRepository.GetAllAsync();
            return entityList;
        }

        public virtual async Task<T> UpdateAsync(T entity, int id)
        {
            var updatedEntity = await _entityRepository.UpdateAsync(entity, id);
            return updatedEntity;
        }

        public async Task<bool> EntityExistsAsync(int id)
        {
            var entityExist = await _entityRepository.EntityExistAsync(id);
            return entityExist;
        }
    }
}
