using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BattleShip.Models;

namespace Application.Services
{
    public interface IUserService
    {
        void AddUser(User user);

        Task<User> GetUserByIdAsync(int id);

        Task<List<User>> GetUsers();

        void RemoveUser(User user);

        void RemoveAll();

         void UpdateUser(int id, User user);
    }
}
