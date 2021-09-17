using RentACarWebAPI.Interfaces;
using RentACarWebAPI.Interfaces.Services;
using RentACarWebAPI.Models;
using System.Collections.Generic;

namespace RentACarWebAPI.Services
{
    public class CarService : ICarService
    {
        private readonly ICarRepository _carRepository;

        public CarService(ICarRepository carRepository)
        {
            _carRepository = carRepository;
        }

        public Car Create(Car car)
        {
            var carCreated = _carRepository.Create(car);
            return carCreated;
        }

        public void Delete(int id)
        {
            _carRepository.Delete(id);
        }

        public Car Get(int id)
        {
            var car = _carRepository.Get(id);
            return car;
        }

        public List<Car> GetAll()
        {
            var carList = _carRepository.GetAll();
            return carList;
        }

        public Car Update(Car car, int id)
        {
            var updatedCar = _carRepository.Update(car, id);
            return updatedCar;
        }
    }
}
