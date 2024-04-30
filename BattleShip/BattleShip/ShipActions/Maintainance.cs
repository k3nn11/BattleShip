using System;
using BattleShip.Field;
using BattleShip.Models;
using BattleShip.ShipImplementation;

namespace BattleShip
{
    public class Maintainance : Radar
    {
        private Ship _ship;

        public Maintainance(Ship ship)
            : base(ship)
        {
            this._ship = ship;
        }

        public bool RepairImplementation(PlayingField playingField, int column, int row)
        {
            bool targetHeal = false;
            var targetCell = playingField.GetCell(column, row);

            var wreckedShips = this.GetShipCells(playingField, targetCell);
            if (wreckedShips == null)
            {
                return targetHeal;
            }

            foreach (var cell in wreckedShips)
            {
                if (cell.Ship.Health < 3)
                {
                    var allCells = playingField.Cells;
                    allCells[cell.Y, cell.X].Ship.Health = 3;
                    targetHeal = true;
                }
            }

            return targetHeal;
        }
    }
}
