using System;

namespace BattleShip.Exceptions
{
    public class FieldArguementException : Exception
    {
        public FieldArguementException()
            : base()
        {
        }

        public FieldArguementException(string message)
            : base(message)
        {
        }
    }
}
