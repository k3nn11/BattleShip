using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BattleShip.Field;
using BattleShip.Models;
using DAL.InMemoryDB;

namespace Application.Services
{
    public class UserService : IUserService
    {
        private IRepository<User> _baseService;

        private int index { get; set; }

        public UserService(IRepository<User> baseService)
        {
            this._baseService = baseService;
        }

        public void AddUser(User user)
        {
            user.Id = index++;
            this._baseService.Add(user);
        }

        public void UpdateUser(int id, User updatedUser)
        {
            this._baseService.Update(id, updatedUser);
        }

        public async Task<User> GetUserByIdAsync(int id)
        {
            var user = await _baseService.GetById(id);
            return user = user.Id == id ? user : null;
        }

        public Task<List<User>> GetUsers()
        {
            return this._baseService.GetAll();
        }

        public void RemoveAll()
        {
            this._baseService.RemoveAll();
        }

        public void RemoveUser(User user)
        {
           this._baseService.Remove(user);
        }

        public void Update(int id, User obj)
        {
            this._baseService.Update(id, obj);
        }
    }
}
