using RentACarWebAPI.Interfaces.Repositories;
using RentACarWebAPI.Models;

namespace RentACarWebAPI.Repositories
{
    public class RentalRepository : BaseRepository<Rental>, IRentalRepository
    {
        public RentalRepository(RentACarDbContext dbContext) : base(dbContext) { }

        protected override void UpdateEntity(Rental existingEntity, Rental newEntity)
        {
            existingEntity.RentalDate = newEntity.RentalDate;
            existingEntity.ReturnDate = newEntity.ReturnDate;
            existingEntity.Car = newEntity.Car;
            existingEntity.Client = newEntity.Client;
        }
    }
}
