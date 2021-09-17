using Microsoft.Extensions.Options;
using RentACarWebAPI.Interfaces;
using RentACarWebAPI.Models;
using RentACarWebAPI.Options;

namespace RentACarWebAPI.Repositories
{
    public class RentalRepository : Repository<Rental>, IRentalRepository
    {
        public RentalRepository(IOptions<StorageOptions> storageConfig) : base(storageConfig.Value.Rental) { }

        protected override void UpdateEntity(Rental existingEntity, Rental newEntity)
        {
            existingEntity.RentalDate = newEntity.RentalDate;
            existingEntity.ReturnDate = newEntity.ReturnDate;
            existingEntity.Car = newEntity.Car;
            existingEntity.Client = newEntity.Client;
        }
    }
}
