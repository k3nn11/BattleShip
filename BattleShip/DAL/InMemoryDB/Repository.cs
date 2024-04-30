using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DAL.InMemoryDB
{
    public class Repository<T> : IRepository<T>
    {
        private List<T> _items;

        public Repository()
        {
           this._items = new List<T>();
        }

        public void Add(T obj)
        {
            this._items.Add(obj);
        }

        public async Task<List<T>> GetAll()
        {
            List<T> list = null;
            await Task.Run(() =>
            {
                list = this._items;
            });

            return list;
        }

        public async Task<T> GetById(int id)
        {
            T item = default(T);
            if (id < 0 || id > _items.Count - 1)
            {
                throw new ArgumentOutOfRangeException(nameof(id));
            }
            await Task.Run(() =>
            {
                item = _items.ElementAt(id);
            }
            );

            return item;
        }

        public void Remove(T obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException(nameof(obj));
            }

            this._items.Remove(obj);
        }

        public void RemoveAll()
        {
            this._items.Clear();
        }

        public async Task Update(int id, T obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException(nameof(obj));
            }

            if (id < 0 || id > _items.Count - 1)
            {
                throw new ArgumentOutOfRangeException(nameof(id));
            }

            T item = await this.GetById(id);
            item = obj;

        }
    }
}
