using RentACarWebAPI.Models;
using System.Collections.Generic;

namespace RentACarWebAPI.Interfaces.Services
{
    public interface ICarService
    {
        Car Create(Car obj);
        Car Get(int id);
        Car Update(Car obj, int id);
        void Delete(int id);
        List<Car> GetAll();
    }
}
