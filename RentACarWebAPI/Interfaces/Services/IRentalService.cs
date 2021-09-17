using RentACarWebAPI.Models;
using System.Collections.Generic;

namespace RentACarWebAPI.Interfaces.Services
{
    public interface IRentalService
    {
        Rental Create(Rental obj);
        Rental Get(int id);
        Rental Update(Rental entity, int id);
        void Delete(int id);
        List<Rental> GetAll();
    }
}
