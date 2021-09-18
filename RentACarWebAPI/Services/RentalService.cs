using RentACarWebAPI.Interfaces.Repositories;
using RentACarWebAPI.Interfaces.Services;
using RentACarWebAPI.Models;

namespace RentACarWebAPI.Services
{
    public class RentalService : BaseService<Rental>, IRentalService
    {
        public RentalService(IRentalRepository rentalRepository) : base(rentalRepository)
        {
        }
    }
}
