using RentACarWebAPI.Models.Base;
using System.Collections.Generic;

namespace RentACarWebAPI.Interfaces
{
    public interface IRepositoryHelper <T> where T : Entity
    {
        List<T> CheckFileAndGetList(string filePath);
        int GetNewId(List<T> objs);
        List<T> GetListFromFile(string filePath);
        void SaveListToFile(List<T> objs, string filePath);
    }
}
