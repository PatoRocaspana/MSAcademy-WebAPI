using RentACarWebAPI.Models;

namespace RentACarWebAPI.Interfaces.Repositories
{
    public interface IClientRepository : IBaseRepository<Client>
    {
        bool DniExist(Client client);
    }
}
