using System;
using System.Collections.Generic;
using BattleShip.Exceptions;
using BattleShip.Models;

namespace BattleShip.Field
{
    public class FieldPopulation : IFieldPlacement
    {
        private PlayingField _playingField;

        private List<Cell> _cells;

        public FieldPopulation(PlayingField playingField)
        {
            this._playingField = playingField;
            this._cells = new List<Cell>();
        }

        public void AddShipInField(int column, int row, Ship ship, Direction? direction)
        {
            if (column < 0 || column > this._playingField.Width || row < 0 || row > this._playingField.Height)
            {
                throw new IndexOutOfRangeException();
            }

            this._cells.Clear();
            this._playingField.Ships.Add(ship);
            Cell cell = this._playingField.GetCell(column, row);
            ship.Head = cell;
            ship.Direction = direction;
            var shipLength = ship.Length;
            var xCordinate = cell.X;
            var yCordinate = cell.Y;
            var occupiedCells = this._playingField.GetAllOccupiedCell();
            Cell occupiedShipCell = null;
            for (int i = 0; i < shipLength; i++)
            {
                try
                {
                    occupiedShipCell = this._playingField.GetCell(xCordinate, yCordinate);
                    var check = this.CellCompare(occupiedShipCell, occupiedCells);
                    if (check)
                    {
                        throw new InvalidShipPlacementException("Ship cells are occupied, ship cannot be Placed in Playing field");
                    }

                    this._cells.Add(occupiedShipCell);
                    if (i == shipLength - 1)
                    {
                        break;
                    }

                    switch (direction)
                    {
                        case Direction.North:
                            yCordinate--;
                            if (yCordinate < 0)
                            {
                                throw new InvalidShipPlacementException("Placing ship goes past edge of Playingfield");
                            }

                            break;
                        case Direction.South:
                            yCordinate++;
                            if (yCordinate > this._playingField.Height)
                            {
                                throw new InvalidShipPlacementException("Placing ship goes past edge of Playingfield");
                            }

                            break;
                        case Direction.East:
                            xCordinate++;
                            if (xCordinate > this._playingField.Width)
                            {
                                throw new InvalidShipPlacementException("Placing ship goes past edge of Playingfield");
                            }

                            break;
                        case Direction.West:
                            xCordinate--;
                            if (xCordinate < 0)
                            {
                                throw new InvalidShipPlacementException("Placing ship goes past edge of Playingfield");
                            }

                            break;
                    }
                }
                catch (Exception)
                {
                    this._cells.Clear();
                    throw;
                }
            }

            ship.Tail = occupiedShipCell;
            this._cells.ForEach(shipCell =>
            {
                this._playingField.OccupyCell(shipCell.Y, shipCell.X, ship);
            });
        }

        private bool CellCompare(Cell cell, List<Cell> cells)
        {
            bool isEqual = false;
            cells.ForEach(x =>
            {
                if (cell.Equals(x))
                {
                    isEqual = true;
                }
            });
            return isEqual;
        }
    }
}
