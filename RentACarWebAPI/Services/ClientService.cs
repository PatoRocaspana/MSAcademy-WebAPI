using RentACarWebAPI.Interfaces.Repositories;
using RentACarWebAPI.Interfaces.Services;
using RentACarWebAPI.Models;
using System.Threading.Tasks;

namespace RentACarWebAPI.Services
{
    public class ClientService : BaseService<Client>, IClientService
    {
        private readonly IClientRepository _clientRepository;

        public ClientService(IClientRepository clientRepository) : base(clientRepository)
        {
            _clientRepository = clientRepository;
        }

        public override async Task<Client> CreateAsync(Client newClient)
        {
            var clientExists = await _clientRepository.DniExistsAsync(newClient);
            if (clientExists) return null;

            var clientCreated = await base.CreateAsync(newClient);

            return clientCreated;
        }

        public override async Task<Client> UpdateAsync(Client newClient, int id)
        {
            var existingClient = await _clientRepository.GetAsync(id);

            if (existingClient is null)
                return null;

            if (existingClient.Dni != newClient.Dni)
            {
                var dniExists = await _clientRepository.DniExistsAsync(newClient);
                if (dniExists)
                    return null;
            }

            var clientUpdated = await base.UpdateAsync(newClient, id);

            return clientUpdated;
        }
    }
}
