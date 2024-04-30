using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BattleShip.Models;

namespace Application.Services
{
    public interface IShipService
    {
        void AddShip(Ship ship);

        void RemoveShip(Ship ship);

        void RemoveAll();

        Task<Ship> GetShipByIdAsync(int id);

        Task<List<Ship>> GetShips();

        void Update(int shipId, Ship updatedShip);
    }
}
