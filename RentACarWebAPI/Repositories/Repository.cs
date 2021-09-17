using RentACarWebAPI.Helpers;
using RentACarWebAPI.Interfaces;
using RentACarWebAPI.Models.Base;
using System.Collections.Generic;
using System.Linq;

namespace RentACarWebAPI.Repositories
{
    public abstract class Repository<T> : IRepository<T> where T : Entity
    {
        protected List<T> EntityList { get; private set; }

        protected readonly string _jsonFile;

        public Repository(string storagePath)
        {
            _jsonFile = storagePath;
            EntityList = RepositoryHelper<T>.CheckFileAndGetList(_jsonFile);
        }

        public virtual T Create(T entity)
        {
            entity.Id = RepositoryHelper<T>.GetNewId(EntityList);
            EntityList.Add(entity);

            RepositoryHelper<T>.SaveListToFile(EntityList, _jsonFile);

            return entity;
        }

        public virtual T Update(T entity, int id)
        {
            var existingEntity = Get(id);

            if (existingEntity is null)
                return null;

            UpdateEntity(existingEntity, entity);

            RepositoryHelper<T>.SaveListToFile(EntityList, _jsonFile);

            return existingEntity;
        }

        protected abstract void UpdateEntity(T existingEntity, T newEntity);

        public virtual T Get(int id)
        {
            var entity = EntityList.FirstOrDefault(e => e.Id == id);
            return entity;
        }

        public virtual void Delete(int id)
        {
            EntityList.Remove(EntityList.FirstOrDefault(obj => obj.Id == id));
            RepositoryHelper<T>.SaveListToFile(EntityList, _jsonFile);
        }

        public virtual List<T> GetAll()
        {
            return EntityList;
        }
    }
}
