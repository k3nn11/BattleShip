using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BattleShip.Models;
using DAL.InMemoryDB;

namespace Application.Services
{
    public class ShipService : IShipService
    {
        private IRepository<Ship> _baseService;

        private int index { get; set; }

        public ShipService(IRepository<Ship> baseService)
        {
            this._baseService = baseService;
        }

        public void AddShip(Ship ship)
        {
            ship.Id = index++;
            this._baseService.Add(ship);
        }

        public async Task<List<Ship>> GetShips()
        {
            return await this._baseService.GetAll();
        }

        public async Task<Ship> GetShipByIdAsync(int id)
        {
            var ship = await _baseService.GetById(id);
            return ship = ship.Id == id ? ship : null;
        }

        public void RemoveShip(Ship ship)
        {
            this._baseService.Remove(ship);
        }

        public void RemoveAll()
        {
            this._baseService.RemoveAll();
        }

        public void Update(int shipId, Ship updatedShip)
        {
           this._baseService.Update(shipId, updatedShip);
        }
    }
}
