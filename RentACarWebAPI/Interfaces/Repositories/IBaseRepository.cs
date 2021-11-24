using RentACarWebAPI.Models.Base;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RentACarWebAPI.Interfaces.Repositories
{
    public interface IBaseRepository<T> where T : Entity
    {
        Task<T> CreateAsync(T obj);
        Task<T> GetAsync(int id);
        Task<T> UpdateAsync(T obj, int id);
        Task DeleteAsync(int id);
        Task<List<T>> GetAllAsync();
        Task<bool> EntityExistAsync(int id);
    }
}
