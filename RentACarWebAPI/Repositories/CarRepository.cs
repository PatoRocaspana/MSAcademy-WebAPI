using Microsoft.Extensions.Options;
using RentACarWebAPI.Interfaces;
using RentACarWebAPI.Models;
using RentACarWebAPI.Options;

namespace RentACarWebAPI.Repositories
{
    public class CarRepository : Repository<Car>, ICarRepository
    {
        public CarRepository(IOptions<StorageOptions> storageConfig) : base(storageConfig.Value.Car) { }

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
