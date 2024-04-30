using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DAL.InMemoryDB
{
    public class UserRepository<TKey, TValue> : IUserRepository<TKey, TValue>
        where TKey : class
        where TValue : class
    {
        private Dictionary<TKey, List<TValue>> _userFields;

        public UserRepository() 
        { 
            _userFields = new Dictionary<TKey, List<TValue>>();
        }
       
        public TKey AddExistingKey(TKey key)
        {
            if (_userFields.ContainsKey(key))
            {
                return key;
            }
            else
            {
                _userFields.Add(key, new List<TValue>());
                return key;
            }
        }

        public void AddExistingValue(TValue value, TKey key)
        {

            if (_userFields.ContainsKey(key))
            {
                _userFields[key].Add(value);
            }
            else
            {
                _userFields.Add(key, new List<TValue>());
                _userFields[key].Add(value);
            }
        }

        public void AddNewValue(TKey key, TValue value)
        {
            if (_userFields.ContainsKey(key))
            {
                _userFields[key].Add(value);
            }
            else
            {
                _userFields.Add(key, new List<TValue>());
                _userFields[key].Add(value);
            }
        }

        public void AddNewKey(TKey key)
        {
            if (_userFields.ContainsKey(key))
            {
                return;
            }
            else
            {
                _userFields.Add(key, new List<TValue> { });
            }
        }

        public async Task<Dictionary<TKey, List<TValue>>> GetAll()
        {
            Dictionary<TKey, List<TValue>> items = null;
            await Task.Run(() =>
            {
                items = _userFields;
            });
            return items;
        }
    }
}
