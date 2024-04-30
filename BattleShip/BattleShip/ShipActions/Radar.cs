using System;
using System.Collections.Generic;
using BattleShip.Field;
using BattleShip.Models;

namespace BattleShip
{
    public class Radar
    {
        private Ship _ship;

        public Radar(Ship ship)
        {
            this._ship = ship;
        }

        protected List<Cell> GetShipCells(PlayingField playingField, Cell targetCell)
        {
            if (!targetCell.IsOccupied)
            {
                return null;
            }

            List<Cell> shipCells = new List<Cell>();
            int column = 0;
            int row = 0;
            foreach (var ship in playingField.Ships)
            {
                if (targetCell.Ship == ship)
                {
                    column = ship.Head.X;
                    row = ship.Head.Y;
                    for (int i = 0; i < ship.Length; i++)
                    {
                        var cell = playingField.GetCell(column, row);
                        shipCells.Add(cell);
                        switch (ship.Direction)
                        {
                            case Direction.North:
                                row--;
                                break;
                            case Direction.South:
                                row++;
                                break;
                            case Direction.East:
                                column++;
                                break;
                            case Direction.West:
                                column--;
                                break;
                        }
                    }
                }
            }

            return shipCells;
        }

        private bool TargetCordinateVerification(Cell targetCell)
        {
            bool verify = targetCell == null || targetCell == this._ship.Head ? false : true;
            return verify;
        }
    }
}
