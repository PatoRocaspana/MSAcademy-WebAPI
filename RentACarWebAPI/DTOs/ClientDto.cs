using RentACarWebAPI.DTOs.Base;
using RentACarWebAPI.Models;
using System;

namespace RentACarWebAPI.DTOs
{
    public class ClientDto : EntityDto
    {
        public string Dni { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Province { get; set; }
        public string PostalCode { get; set; }
        public DateTime LastUpdate { get; set; }

        public ClientDto() { }

        public ClientDto(Client clientEntity)
        {
            Id = clientEntity.Id;
            Dni = clientEntity.Dni;
            Name = clientEntity.Name;
            LastName = clientEntity.LastName;
            PhoneNumber = clientEntity.PhoneNumber;
            Address = clientEntity.Address;
            City = clientEntity.City;
            Province = clientEntity.Province;
            PostalCode = clientEntity.PostalCode;
            LastUpdate = clientEntity.LastUpdate;
        }

        public Client ToClientEntity(ClientDto clientDto)
        {
            var clientEntity = new Client()
            {
                Id = clientDto.Id,
                Dni = clientDto.Dni,
                Name = clientDto.Name,
                LastName = clientDto.LastName,
                PhoneNumber = clientDto.PhoneNumber,
                Address = clientDto.Address,
                City = clientDto.City,
                Province = clientDto.Province,
                PostalCode = clientDto.PostalCode,
                LastUpdate = clientDto.LastUpdate
            };

            return clientEntity;
        }
    }
}
