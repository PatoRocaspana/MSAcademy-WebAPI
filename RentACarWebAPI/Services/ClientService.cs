using RentACarWebAPI.Interfaces;
using RentACarWebAPI.Interfaces.Services;
using RentACarWebAPI.Models;
using System.Collections.Generic;

namespace RentACarWebAPI.Services
{
    public class ClientService : IClientService
    {
        private readonly IClientRepository _clientRepository;

        public ClientService(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
        }

        public Client Create(Client client)
        {
            var clientCreated = _clientRepository.Create(client);
            return clientCreated;
        }

        public void Delete(int id)
        {
            _clientRepository.Delete(id);
        }

        public Client Get(int id)
        {
            var client = _clientRepository.Get(id);
            return client;
        }

        public List<Client> GetAll()
        {
            var clientList = _clientRepository.GetAll();
            return clientList;
        }

        public Client Update(Client client, int id)
        {
            var updatedClient = _clientRepository.Update(client, id);
            return updatedClient;
        }
    }
}
