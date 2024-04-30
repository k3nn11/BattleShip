using System;
using System.Collections.Generic;
using BattleShip.Field;
using BattleShip.Models;
using DAL.InMemoryDB;

namespace BattleShip.Models
{
    public class User : Base
    {
        public User()
        {
            this.FieldShipPairs = new Dictionary<PlayingField, List<Ship>>();
        }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public Role Role { get; set; }

        public List<Ship> Ships { get; set; }

        public IUserRepository<PlayingField, Ship> UserRepository { get; set; } = new UserRepository<PlayingField, Ship>();

        public Dictionary<PlayingField, List<Ship>> FieldShipPairs { get; set; }

        public PlayingField PlayingField { get; set; }

        public override string ToString()
        {
            return this.FirstName;
        }

        public void AddOrUpdate(PlayingField field, List<Ship> ships)
        {
            if (this.FieldShipPairs.ContainsKey(field))
            {
                this.FieldShipPairs[field] = ships;
            }
            else
            {
                this.FieldShipPairs.Add(field, ships);
            }
        }
    }
}
