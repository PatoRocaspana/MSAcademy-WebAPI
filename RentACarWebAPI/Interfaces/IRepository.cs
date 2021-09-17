﻿using RentACarWebAPI.Models.Base;
using System.Collections.Generic;

namespace RentACarWebAPI.Interfaces
{
    public interface IRepository<T> where T : Entity
    {
        T Create(T obj);
        T Get(int id);
        T Update(T obj, int id);
        void Delete(int id);
        List<T> GetAll();
    }
}
