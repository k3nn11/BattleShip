using System;

namespace BattleShip.Exceptions
{
    public class InvalidMovementException : Exception
    {
        public InvalidMovementException()
            : base()
        {
        }

        public InvalidMovementException(string message) 
            : base(message)
        {
        }
    }
}
