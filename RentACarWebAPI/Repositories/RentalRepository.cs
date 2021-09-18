using Microsoft.Extensions.Options;
using RentACarWebAPI.Interfaces.Repositories;
using RentACarWebAPI.Models;
using RentACarWebAPI.Options;

namespace RentACarWebAPI.Repositories
{
    public class RentalRepository : BaseRepository<Rental>, IRentalRepository
    {
        public RentalRepository(IOptions<StorageOptions> storageConfig, IRepositoryHelper<Rental> repositoryHelper) : base(storageConfig.Value.Rental, repositoryHelper) { }

        protected override void UpdateEntity(Rental existingEntity, Rental newEntity)
        {
            existingEntity.RentalDate = newEntity.RentalDate;
            existingEntity.ReturnDate = newEntity.ReturnDate;
            existingEntity.Car = newEntity.Car;
            existingEntity.Client = newEntity.Client;
        }
    }
}
