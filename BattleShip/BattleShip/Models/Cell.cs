namespace BattleShip.Models
{
    public class Cell
    {
        public Cell(int y, int x)
        {
            this.X = x;
            this.Y = y;
            this.IsOccupied = false;
        }

        public bool IsOccupied { get; set; }

        public Ship Ship { get; set; }

        public int X { get; }

        public int Y { get; }

        public override string ToString()
        {
            return "[] ";
        }
    }
}