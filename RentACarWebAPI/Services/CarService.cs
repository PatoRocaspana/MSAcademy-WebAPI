using RentACarWebAPI.Interfaces.Repositories;
using RentACarWebAPI.Interfaces.Services;
using RentACarWebAPI.Models;

namespace RentACarWebAPI.Services
{
    public class CarService : BaseService<Car>, ICarService
    {
        public CarService(ICarRepository carRepository) : base(carRepository)
        {
        }
    }
}
