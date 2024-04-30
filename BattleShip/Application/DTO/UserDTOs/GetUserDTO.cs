using BattleShip.Models;
using System;

namespace Application.DTO.UserDTO
{
    public class GetUserDTO : Base
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Role { get; set; }
    }
}
