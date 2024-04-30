using System;

namespace BattleShip.DTO.ShipDTO
{
    public interface IBaseRequestDTO
    {
        public int Length { get; set; }

        public int Health { get; set; }
    }
}
