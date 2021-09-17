using Microsoft.Extensions.Options;
using RentACarWebAPI.Helpers;
using RentACarWebAPI.Interfaces;
using RentACarWebAPI.Models;
using RentACarWebAPI.Options;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RentACarWebAPI.Repositories
{
    public class ClientRepository : Repository<Client>, IClientRepository
    {
        public ClientRepository(IOptions<StorageOptions> storageConfig, IRepositoryHelper<Client> repositoryHelper) : base(storageConfig.Value.Client, repositoryHelper) { }

        public override Client Create(Client newEntity)
        {
            if (DniExistInList(newEntity.Dni))
                return null;

            newEntity.LastUpdate = DateTime.UtcNow;

            var clientCreated = base.Create(newEntity);

            return clientCreated;
        }

        public override Client Update(Client newEntity, int id)
        {
            var existingEntity = Get(id);

            if (existingEntity is null)
                return null;

            if (existingEntity.Dni != newEntity.Dni)
            {
                if (DniExistInList(newEntity.Dni))
                    return null;
            }

            UpdateEntity(existingEntity, newEntity);

            RepositoryHelper.SaveListToFile(EntityList, _jsonFile);

            return existingEntity;
        }

        public override List<Client> GetAll()
        {
            var list = EntityList
                           .OrderBy(e => e.Id)
                           .ToList();
            return list;
        }

        protected override void UpdateEntity(Client existingEntity, Client newEntity)
        {
            existingEntity.Dni = newEntity.Dni;
            existingEntity.Name = newEntity.Name;
            existingEntity.LastName = newEntity.LastName;
            existingEntity.PhoneNumber = newEntity.PhoneNumber;
            existingEntity.City = newEntity.City;
            existingEntity.Address = newEntity.Address;
            existingEntity.PostalCode = newEntity.PostalCode;
            existingEntity.Province = newEntity.Province;
            existingEntity.LastUpdate = DateTime.UtcNow;
        }

        private bool DniExistInList(string dni)
        {
            return EntityList.Any(e => e.Dni == dni);
        }
    }
}
