using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DAL.InMemoryDB
{
    public interface IRepository<T>
    {
        void Add(T obj);

        Task<T> GetById(int id);

        Task<List<T>> GetAll();

        void Remove(T obj);

        Task Update(int id, T obj);

        void RemoveAll();

    }
}
