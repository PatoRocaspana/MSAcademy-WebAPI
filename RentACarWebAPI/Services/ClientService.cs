using RentACarWebAPI.Interfaces.Repositories;
using RentACarWebAPI.Interfaces.Services;
using RentACarWebAPI.Models;

namespace RentACarWebAPI.Services
{
    public class ClientService : BaseService<Client>, IClientService
    {
        public ClientService(IClientRepository clientRepository) : base(clientRepository)
        {
        }
    }
}
