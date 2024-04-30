namespace BattleShip.Models
{
    public class Mixed : Ship
    {
        public Mixed(int length, int health)
            : base(health)
        {
            this.Length = length;
            this.Weapon = new Weapon(this);
            this.Maintainance = new Maintainance(this);
        }

        public Mixed(int length, int speed, int health)
            : base(health)
        {
            this.Speed = speed;
            this.Length = length;
            this.Weapon = new Weapon(this);
            this.Maintainance = new Maintainance(this);
        }

        public Weapon Weapon { get; }

        public Maintainance Maintainance { get; }
    }
}
