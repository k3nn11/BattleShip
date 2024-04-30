namespace BattleShip.Models
{
    public class Auxilliary : Ship
    {
        public Auxilliary(int length, int health)
            : base(health)
        {
            this.Length = length;
            this.Maintainance = new Maintainance(this);
        }

        public Auxilliary(int length, int speed, int health)
            : base(health)
        {
            this.Speed = speed;
            this.Length = length;
            this.Maintainance = new Maintainance(this);
        }

        public Maintainance Maintainance { get; }
    }
}
