using RentACarWebAPI.Interfaces;
using RentACarWebAPI.Interfaces.Services;
using RentACarWebAPI.Models;
using System.Collections.Generic;

namespace RentACarWebAPI.Services
{
    public class RentalService : IRentalService
    {
        private readonly IRentalRepository _rentalRepository;

        public RentalService(IRentalRepository rentalRepository)
        {
            _rentalRepository = rentalRepository;
        }

        public Rental Create(Rental rental)
        {
            var rentalCreated = _rentalRepository.Create(rental);
            return rentalCreated;
        }

        public void Delete(int id)
        {
            _rentalRepository.Delete(id);
        }

        public Rental Get(int id)
        {
            var rental = _rentalRepository.Get(id);
            return rental;
        }

        public List<Rental> GetAll()
        {
            var rentalList = _rentalRepository.GetAll();
            return rentalList;
        }

        public Rental Update(Rental rental, int id)
        {
            var updatedRental = _rentalRepository.Update(rental, id);
            return updatedRental;
        }
    }
}
