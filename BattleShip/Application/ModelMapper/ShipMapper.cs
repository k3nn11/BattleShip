using System;
using System.Linq;
using System.Reflection;
using Application.DTO.ShipDTO;
using BattleShip.Models;

namespace Application.ModelMapper
{
    public static class ShipMapper
    {
        public static GetShipDTO MapToDTO(Ship ship)
        {
            Type type = ship.GetType();
            if (type == typeof(Auxilliary))
            {
                return new AuxilliaryDTO
                {
                    Id = ship.Id,
                    Speed = ship.Speed,
                    Length = ship.Length,
                    Health = ship.Health,
                    ShipType = ship.GetType().Name,
                };
            }
            else if (type == typeof(Military))
            {
                return new MilitaryDTO
                {
                    Id = ship.Id,
                    Speed = ship.Speed,
                    Length = ship.Length,
                    Health = ship.Health,
                    ShipType = ship.GetType().Name,
                };
            }
            else if (type == typeof(Mixed))
            {
                return new MixedDTO
                {
                    Id = ship.Id,
                    Speed = ship.Speed,
                    Length = ship.Length,
                    Health = ship.Health,
                    ShipType = ship.GetType().Name,
                };
            }
            else
            {
                throw new ArgumentException("Invalid ship type");
            }
        }

        public static Ship PostMapToModel(PostShipDTO shipDTO)
        {
            var shipDTOType = shipDTO.ShipType;
            if (shipDTOType == ShipType.Auxilliary)
            {
                return new Auxilliary(shipDTO.Length, shipDTO.Health)
                {
                    Health = shipDTO.Health,
                    Length = shipDTO.Length,
                    Speed = shipDTO.Speed,
                };
            }
            else if (shipDTOType == ShipType.Military)
            {
                return new Military(shipDTO.Length, shipDTO.Health)
                {
                    Health = shipDTO.Health,
                    Length = shipDTO.Length,
                    Speed = shipDTO.Speed
                };
            }
            else if (shipDTOType == ShipType.Mixed)
            {
                return new Mixed(shipDTO.Length, shipDTO.Health)
                {
                    Health = shipDTO.Health,
                    Length = shipDTO.Length,
                    Speed = shipDTO.Speed
                };
            }
            else
            {
                throw new ArgumentException("Invalid shipDTO type");
            }
        }

        public static Ship PutMapToModel(PutShipDTO shipDTO,  Ship ship)
        {
            ship.Health = shipDTO.Health;
            ship.Length = shipDTO.Length;
            ship.Speed = shipDTO.Speed;
            return ship;
        }
    }
}
