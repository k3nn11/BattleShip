using BattleShip.Models;
using System;

namespace Application.DTO.ShipDTO
{
    public class GetShipDTO : BaseShipDTO
    {
        public int Id { get; set; }

        public string ShipType { get; set; }
    }
}
