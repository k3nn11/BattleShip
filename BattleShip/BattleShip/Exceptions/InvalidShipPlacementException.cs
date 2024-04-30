using System;

namespace BattleShip.Exceptions
{
    public class InvalidShipPlacementException : Exception
    {
        public InvalidShipPlacementException()
            : base()
        {
        }

        public InvalidShipPlacementException(string message)
            : base(message)
        {
        }
    }
}
