using RentACarWebAPI.Interfaces.Repositories;
using RentACarWebAPI.Interfaces.Services;
using RentACarWebAPI.Models;

namespace RentACarWebAPI.Services
{
    public class ClientService : BaseService<Client>, IClientService
    {
        private readonly IClientRepository _clientRepository;

        public ClientService(IClientRepository clientRepository) : base(clientRepository)
        {
            _clientRepository = clientRepository;
        }

        public override Client Create(Client newClient)
        {
            var clientExists = _clientRepository.DniExist(newClient);
            if (clientExists) return null;

            var clientCreated = base.Create(newClient);

            return clientCreated;
        }

        public override Client Update(Client newClient, int id)
        {
            var existingClient = _clientRepository.Get(id);

            if (existingClient is null)
                return null;

            if (existingClient.Dni != newClient.Dni)
            {
                var DniExist = _clientRepository.DniExist(newClient);
                if (DniExist)
                    return null;
            }

            var clientUpdated = base.Update(newClient, id);

            return clientUpdated;
        }
    }
}
