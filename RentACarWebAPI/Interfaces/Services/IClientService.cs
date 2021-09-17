using RentACarWebAPI.Models;
using System.Collections.Generic;

namespace RentACarWebAPI.Interfaces.Services
{
    public interface IClientService
    {
        Client Create(Client obj);
        Client Get(int id);
        Client Update(Client entity, int id);
        void Delete(int id);
        List<Client> GetAll();
    }
}
