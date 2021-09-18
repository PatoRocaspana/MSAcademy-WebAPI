using Microsoft.Extensions.Options;
using RentACarWebAPI.Interfaces.Repositories;
using RentACarWebAPI.Models;
using RentACarWebAPI.Options;

namespace RentACarWebAPI.Repositories
{
    public class CarRepository : BaseRepository<Car>, ICarRepository
    {
        public CarRepository(IOptions<StorageOptions> storageConfig, IRepositoryHelper<Car> repositoryHelper) : base(storageConfig.Value.Car, repositoryHelper) { }

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
