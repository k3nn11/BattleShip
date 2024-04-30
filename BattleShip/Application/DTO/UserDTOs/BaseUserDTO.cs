using BattleShip.Models;
using System;

namespace Application.DTO.UserDTO
{
    public class BaseUserDTO
    {
        public Role Role { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }
    }
}
