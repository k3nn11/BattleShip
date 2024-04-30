using System;
using System.Collections.Generic;
using System.Runtime.ExceptionServices;
using BattleShip.Exceptions;
using BattleShip.FieldEnum;
using BattleShip.Models;
using BattleShip.ShipImplementation;

namespace BattleShip.Field
{
    public class PlayingField : Base
    {
        private readonly int _max = 100;

        private readonly int _min = 5;

        public PlayingField(int height, int width)
        {
            this.Height = height;
            this.Width = width;
            this.CreateField();
            this.FieldPlacement = new FieldPopulation(this);
            this.Movement = new Movement(this);
        }

        public FieldPopulation FieldPlacement { get; set; }

        public string Name { get; set; }

        public Movement Movement { get; set; }

        public Cell[,] Cells { get; set; }

        public int Height { get; set; }

        public int Width { get; set; }

        public List<Ship> Ships { get; set; } = new List<Ship>();

        public Cell this[Quadrant quadrant, int column, int row]
        {
            get
            {
                Cell cell = this.ConvertIndex(quadrant, column, row);
                return cell;
            }
        }

        public void OccupyCell(int column, int row, Ship ship)
        {
            Cell cell = this.GetCell(row, column);
            if (cell != null)
            {
                cell.Ship = ship;
                cell.IsOccupied = true;
            }
        }

        public void DisplayField()
        {
            var horizontalLength = 0;
            var index = this.Height;
            for (int i = this.Height - 1; i >= 0; i--)
            {
                for (int j = 0; j < this.Width; j++)
                {
                    if (this.Width == horizontalLength)
                    {
                        Console.Write("\n");
                        horizontalLength = 0;
                    }

                    if (horizontalLength == 0)
                    {
                        Console.Write($"{index--}  ");
                    }

                    horizontalLength++;
                    var totalCells = this.Height * this.Width;
                    if (this.Cells[i, j].IsOccupied)
                    {
                        var shiptype = this.Cells[i, j].Ship.GetType().Name;
                        if (shiptype == "Military")
                        {
                            Console.ForegroundColor = ConsoleColor.Black;
                            Console.BackgroundColor = ConsoleColor.Red;
                        }
                        else if (shiptype == "Auxilliary")
                        {
                            Console.ForegroundColor = ConsoleColor.Black;
                            Console.BackgroundColor = ConsoleColor.Green;
                        }
                        else if (shiptype == "Mixed")
                        {
                            Console.ForegroundColor = ConsoleColor.Black;
                            Console.BackgroundColor = ConsoleColor.Blue;
                        }
                    }

                    Console.Write(this.Cells[i, j]);
                    Console.ResetColor();
                }
            }

            Console.WriteLine();
            for (int i = 1; i <= this.Width; i++)
            {
                if (i == 1)
                {
                    Console.Write("  ");
                }

                Console.Write("  " + i);
            }

            Console.WriteLine();
        }

        public void DisplayShips()
        {
            int index = 0;
            foreach (var ship in this.Ships)
            {
                Console.WriteLine($"{index}: {ship}");
                index++;
            }
        }

        public Cell GetCell(int column, int row)
        {
            if (row >= 0 && row < this.Width && column >= 0 && column < this.Height)
            {
                return this.Cells[row, column];
            }

            return null;
        }

        public void CleanUp(int column, int row)
        {
            Cell cell = this.GetCell(column, row);
            cell.Ship = null;
            cell.IsOccupied = false;
        }

        public bool Check(int column, int row)
        {
            Cell cell = this.GetCell(column, row);
            bool confirm = cell == null || cell.IsOccupied ? false : true;
            return confirm;
        }

        public List<Cell> GetAllOccupiedCell()
        {
            List<Cell> cellList = new List<Cell>();
            foreach (var item in this.Cells)
            {
                if (item.IsOccupied)
                {
                    cellList.Add(item);
                }
            }

            return cellList;
        }

        public List<Ship> ShipSort()
        {
            List<Ship> sortedShipList = new List<Ship>();
            var shipCells = this.GetAllOccupiedCell();
            var n = shipCells.Count;
            bool swapped;
            for (int i = 0; i < n - 1; i++)
            {
                swapped = false;
                for (int j = 0; j < n - i - 1; j++)
                {
                    if (this.DistanceFromCentre(shipCells[j]) > this.DistanceFromCentre(shipCells[j + 1]))
                    {
                        var temp = shipCells[j];
                        shipCells[j] = shipCells[j + 1];
                        shipCells[j + 1] = temp;
                        swapped = true;
                    }
                }

                if (swapped == false)
                {
                    break;
                }
            }

            sortedShipList = this.FilterSortedCell(shipCells);
            return sortedShipList;
        }

        public override string ToString()
        {
            return $"Name: {this.Name}, Height: {this.Height}, Width: {this.Width}";
        }

        public void AdjustCordinate(ref int column, ref int row)
        {
            column--;
            row--;
        }

        private List<Ship> FilterSortedCell(List<Cell> cells)
        {
            List<Ship> ships = new List<Ship>();
            foreach (var item in cells)
            {
                if (!ships.Exists(x => item.Ship.Head == x.Head))
                {
                    ships.Add(item.Ship);
                }
            }

            return ships;
        }

        private double DistanceFromCentre(Cell cell)
        {
            double result;
            Cell centre = this.GetCell((this.Height - 1) / 2, (this.Width - 1) / 2);
            var a = centre.X - cell.X;
            var b = centre.Y - cell.Y;
            result = Math.Sqrt(Math.Pow(a, 2) + Math.Pow(b, 2));
            return result;
        }

        private Cell ConvertIndex(Quadrant quadrant, int column, int row)
        {
            switch (quadrant)
            {
                case Quadrant.First:
                    column = (this.Width / 2) + column;
                    row = (this.Height / 2) + row;
                    if (column > this.Width && row > this.Height)
                    {
                        throw new IndexOutOfRangeException();
                    }

                    return this.Cells[column, row];
                case Quadrant.Second:
                    row = (this.Height / 2) + row;
                    if (column < 0 && column > this.Width && row > this.Height)
                    {
                        throw new IndexOutOfRangeException();
                    }

                    return this.Cells[column, row];
                case Quadrant.Third:
                    if (column < 0 && column > (this.Width / 2) && row < 0 && row > (this.Height / 2))
                    {
                        throw new IndexOutOfRangeException();
                    }

                    return this.Cells[column, row];
                case Quadrant.Forth:
                    column = (this.Width / 2) + column;
                    if (column > this.Width && row < 0 && row > (this.Height / 2))
                    {
                        throw new IndexOutOfRangeException();
                    }

                    return this.Cells[column, row];
            }

            throw new Exception("Enter a valid quadrant");
        }

        private void CreateField()
        {
            if (this.Height % 2 == 0 || this.Width % 2 == 0)
            {
                throw new FieldArguementException($"You entered Height: {this.Height} and Width: {this.Width}. PlayingField dimensions should be odd numbers.");
            }

            this.Cells = new Cell[this.Height, this.Width];
            var total = this.Height * this.Width;
            if (total < this._min || total > this._max)
            {
                throw new FieldArguementException($"You created {total} cells. The minimum acceptable is: {this._min}. The maximum acceptable is: {this._max}");
            }

            for (int i = 0; i < this.Height; i++)
            {
                for (int j = 0; j < this.Width; j++)
                {
                    this.Cells[i, j] = new Cell(i, j);
                }
            }
        }
    }
}
