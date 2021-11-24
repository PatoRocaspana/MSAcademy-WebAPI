using RentACarWebAPI.Models;
using System.Threading.Tasks;

namespace RentACarWebAPI.Interfaces.Repositories
{
    public interface IClientRepository : IBaseRepository<Client>
    {
        Task<bool> DniExistsAsync(Client client);
    }
}
