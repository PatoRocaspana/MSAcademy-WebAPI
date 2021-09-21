using RentACarWebAPI.Interfaces.Repositories;
using RentACarWebAPI.Models.Base;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace RentACarWebAPI.Helpers
{
    public class RepositoryHelper<T> : IRepositoryHelper<T> where T : Entity
    {
        /// <summary>
        /// Takes a file and deserialize it to a List<T>.
        /// In the case that file does not exist, it returns a new empty List<T>.
        /// </summary>
        public List<T> CheckFileAndGetList(string filePath)
        {
            var objs = (!File.Exists(filePath)) ? new List<T>() : GetListFromFile(filePath);
            return objs;
        }

        public int GetNewId(List<T> objs)
        {
            if (objs.Count > 0)
                return objs.Max(e => e.Id) + 1;
            else
                return 1;
        }

        public List<T> GetListFromFile(string filePath)
        {
            using (var streamReader = File.OpenText(filePath))
            {
                var jsonContent = streamReader.ReadToEnd();
                List<T> deserializedJson = JsonSerializer.Deserialize<List<T>>(jsonContent);
                return deserializedJson;
            }
        }

        public void SaveListToFile(List<T> objs, string filePath)
        {
            var options = new JsonSerializerOptions { WriteIndented = true };

            using (var streamWriter = File.CreateText(filePath))
            {
                var listToString = JsonSerializer.Serialize(objs, options);
                streamWriter.Write(listToString);
            }
        }
    }
}
