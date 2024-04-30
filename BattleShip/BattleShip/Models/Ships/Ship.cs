using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using BattleShip.Field;

namespace BattleShip.Models
{
    public abstract class Ship : Base, IEquatable<Ship>
    {
        private int _speed;

        [JsonConstructor]
        public Ship(int health)
        {
            this.Speed = 2;
            this.Health = health;
        }

        public Cell Head { get; set; }

        public Cell Tail { get; set; }

        public Direction? Direction { get; set; }

        public int Speed
        {
            get
            {
                return this._speed;
            }

            set
            {
                if (value > 0 && value <= 5)
                {
                    this._speed = value;
                }
            }
        }

        public int Health { get; set; }

        public int Length { get; set; }

        public static bool operator ==(Ship left, Ship right)
            {
            if (ReferenceEquals(left, right))
            {
                return true;
            }

            if (ReferenceEquals(right, null) || ReferenceEquals(null, left))
            {
                return false;
            }

            return left.Equals(right);
            }

        public static bool operator !=(Ship left, Ship right)
        {
            return !(left == right);
        }

        public bool Equals(Ship other)
        {
            return other != null && this.Speed == other.Speed && this.Length == other.Length & this.GetType() == other.GetType();
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return $" Type: {this.GetType().FullName}, Length: {this.Length}, Speed: {(int)this.Speed}, Direction: {this.Direction}, Health: {this.Health}";
        }
    }
}
