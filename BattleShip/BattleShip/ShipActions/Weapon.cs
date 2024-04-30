using BattleShip.Field;
using BattleShip.Models;

namespace BattleShip
{
    public class Weapon : Radar
    {
        public Weapon(Ship ship)
            : base(ship)
        {
        }

        public bool ShootImplementation(PlayingField playingField, int column, int row)
        {
            bool targetHit = false;
            Cell targetCell = playingField.GetCell(column, row);
            var enemyShipCells = this.GetShipCells(playingField, targetCell);

            if (enemyShipCells == null)
            {
                return targetHit;
            }

            targetHit = true;
            targetCell.Ship.Health--;

            return targetHit;
        }
    }
}
