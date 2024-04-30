using BattleShip.Models;
using System;
using System.Collections.Generic;

namespace Application.DTO
{
    public class FieldStateDTO : Base
    {
        public string FieldName { get; set; }

        public int Height { get; set; }

        public int Width { get; set; }

        public List<Ship> Ships {  get; set; }
    }
}
