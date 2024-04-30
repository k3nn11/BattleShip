using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DAL.InMemoryDB
{
    public interface IUserRepository<TKey ,TValue>
        where TKey : class
        where TValue : class
    {
        TKey AddExistingKey(TKey key);

        void AddNewKey(TKey key);

        Task<Dictionary<TKey, List<TValue>>> GetAll();

        void AddExistingValue(TValue value, TKey key);

        void AddNewValue(TKey key, TValue ship);
    }
}
