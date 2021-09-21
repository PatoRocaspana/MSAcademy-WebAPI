using Microsoft.Extensions.Options;
using RentACarWebAPI.Interfaces.Repositories;
using RentACarWebAPI.Models;
using RentACarWebAPI.Options;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RentACarWebAPI.Repositories
{
    public class ClientRepository : BaseRepository<Client>, IClientRepository
    {
        public ClientRepository(IOptions<StorageOptions> storageConfig, IRepositoryHelper<Client> repositoryHelper) : base(storageConfig.Value.Client, repositoryHelper) { }

        public override Client Create(Client newEntity)
        {
            newEntity.LastUpdate = DateTime.UtcNow;

            var clientCreated = base.Create(newEntity);

            return clientCreated;
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
    }
}
