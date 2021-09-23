using RentACarWebAPI.Interfaces.Repositories;
using RentACarWebAPI.Interfaces.Services;
using RentACarWebAPI.Models;
using System.Linq;

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
            var clientList = _clientRepository.GetAll();

            if (clientList.Any(e => e.Dni == newClient.Dni))
                return null;

            var clientCreated = base.Create(newClient);

            return clientCreated;
        }

        public override Client Update(Client newClient, int id)
        {
            var existingClient = _clientRepository.Get(id);

            if (existingClient is null)
                return null;

            var clientList = _clientRepository.GetAll();

            if (existingClient.Dni != newClient.Dni)
            {
                if (clientList.Any(e => e.Dni == newClient.Dni))
                    return null;
            }

            var clientUpdated = base.Update(newClient, id);

            return clientUpdated;
        }
    }
}
