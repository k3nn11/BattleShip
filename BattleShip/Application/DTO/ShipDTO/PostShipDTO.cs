using System;
using Application.DTO.ShipDTO;
using BattleShip.Models;

namespace Application.DTO.ShipDTO
{
    public class PostShipDTO : BaseShipDTO
    {
        public ShipType ShipType { get; set; }
    }
}
