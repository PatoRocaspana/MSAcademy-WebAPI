using RentACarWebAPI.Interfaces.Repositories;
using RentACarWebAPI.Models;

namespace RentACarWebAPI.Repositories
{
    public class CarRepository : BaseRepository<Car>, ICarRepository
    {
        public CarRepository(RentACarDbContext dbContext) : base(dbContext) { }

        protected override void UpdateEntity(Car existingEntity, Car newEntity)
        {
            existingEntity.Brand = newEntity.Brand;
            existingEntity.Color = newEntity.Color;
            existingEntity.DoorsQuantity = newEntity.DoorsQuantity;
            existingEntity.Model = newEntity.Model;
            existingEntity.Transmission = newEntity.Transmission;
        }
    }
}
