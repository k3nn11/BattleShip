namespace BattleShip.Models
{
    public class Military : Ship
    {
        public Military(int length, int health)
            : base(health)
        {
            this.Length = length;
            this.Weapon = new Weapon(this);
        }

        public Military(int length, int speed, int health)
            : base(health)
        {
            this.Speed = speed;
            this.Length = length;
            this.Weapon = new Weapon(this);
        }

        public Weapon Weapon { get; }
    }
}
