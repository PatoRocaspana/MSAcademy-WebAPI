using Microsoft.EntityFrameworkCore;
using RentACarWebAPI.Interfaces.Repositories;
using RentACarWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RentACarWebAPI.Repositories
{
    public class ClientRepository : BaseRepository<Client>, IClientRepository
    {
        public ClientRepository(RentACarDbContext dbContext) : base(dbContext) { }

        public override async Task<Client> CreateAsync(Client newEntity)
        {
            newEntity.LastUpdate = DateTime.UtcNow;

            var clientCreated = await base.CreateAsync(newEntity);

            return clientCreated;
        }

        public override async Task<List<Client>> GetAllAsync()
        {
            var orderClientList = await DbContext.Clients.OrderBy(e => e.Id).ToListAsync();

            return orderClientList;
        }

        public async Task<bool> DniExistsAsync(Client client)
        {
            var exists = await DbContext.Clients.AnyAsync(o => o.Dni == client.Dni);
            return exists;
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
