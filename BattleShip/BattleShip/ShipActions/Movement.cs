using System;
using BattleShip.Exceptions;
using BattleShip.Field;
using BattleShip.Models;

namespace BattleShip.ShipImplementation
{
    public class Movement
    {
        private PlayingField _playingField;

        public Movement(PlayingField playingField)
        {
            this._playingField = playingField;
        }

        public void Forward(Ship ship)
        {
            var forwardSpeed = ship.Speed;
            this.Move(forwardSpeed, ship);
        }

        public void Reverse(Ship ship)
        {
            var reverseSpeed = -1;
            this.Move(reverseSpeed, ship);
        }

        private void Move(int speed, Ship ship)
        {
            var shipLength = ship.Length;
            var head = speed < 0 ? ship.Tail : ship.Head;
            int yCordinate = head.Y, xCordinate = head.X;
            var headYBuffer = yCordinate;
            var headXBuffer = xCordinate;
            int xCordinateCheck = 0;
            int yCordinateCheck = 0;
            int nextCell = 0;
            var absoluteSpeed = Math.Abs(speed);
            do
            {
                var yBuffer = yCordinate;
                var xBuffer = xCordinate;
                for (int i = 0; i < shipLength; i++)
                {
                    switch (ship.Direction)
                    {
                        case Direction.North:
                            this.NorthDirection(speed, ship, ref yCordinate, xCordinate, ref headYBuffer, headXBuffer, out xCordinateCheck, out yCordinateCheck, absoluteSpeed, ref yBuffer, xBuffer, out nextCell);
                            break;
                        case Direction.South:
                            this.SouthDirection(speed, ship, ref yCordinate, xCordinate, ref headYBuffer, headXBuffer, out xCordinateCheck, out yCordinateCheck, absoluteSpeed, ref yBuffer, xBuffer, out nextCell);
                            break;
                        case Direction.West:
                            this.WestDirection(speed, ship, yCordinate, ref xCordinate, headYBuffer, ref headXBuffer, out xCordinateCheck, out yCordinateCheck, absoluteSpeed, yBuffer, ref xBuffer, out nextCell);
                            break;
                        case Direction.East:
                            this.EastDirection(speed, ship, yCordinate, ref xCordinate, headYBuffer, ref headXBuffer, out xCordinateCheck, out yCordinateCheck, absoluteSpeed, yBuffer, ref xBuffer, out nextCell);
                            break;
                    }
                }

                xCordinate = headXBuffer;
                yCordinate = headYBuffer;
            }
            while (this.CanMoveToPosition(xCordinate, yCordinate));
        }

        private void EastDirection(int speed, Ship ship, int yCordinate, ref int xCordinate, int headYBuffer, ref int headXBuffer, out int xCordinateCheck, out int yCordinateCheck, int absoluteSpeed, int yBuffer, ref int xBuffer, out int nextCell)
        {
            xCordinate -= speed;
            headXBuffer = speed < 0 ? (headXBuffer > xCordinate ? headXBuffer : xCordinate) : (headXBuffer < xCordinate ? headXBuffer : xCordinate);
            if (!this.CanMoveToPosition(nextCell = speed > 0 ? xBuffer - 1 : xCordinate, yCordinate) || !this.CanMoveToPosition(xCordinate, yCordinate))
            {
                throw new InvalidMovementException($"{ship}. Cannot Move to Column: {xCordinate}, row: {yCordinate}");
            }

            this._playingField.OccupyCell(yCordinate, xCordinate, ship);
            this._playingField.CleanUp(xBuffer, yBuffer);
            ship.Tail = speed < 0 ? this._playingField.GetCell(headXBuffer, yCordinate) : this._playingField.GetCell(xCordinate, yBuffer);
            ship.Head = speed < 0 ? this._playingField.GetCell(xCordinate, yBuffer) : this._playingField.GetCell(headXBuffer, yBuffer);
            xBuffer = speed > 0 ? ++xBuffer : --xBuffer;
            xCordinate = xBuffer;
            xCordinateCheck = speed > 0 ? headXBuffer - absoluteSpeed : headXBuffer + absoluteSpeed;
            yCordinateCheck = headYBuffer;
        }

        private void WestDirection(int speed, Ship ship, int yCordinate, ref int xCordinate, int headYBuffer, ref int headXBuffer, out int xCordinateCheck, out int yCordinateCheck, int absoluteSpeed, int yBuffer, ref int xBuffer, out int nextCell)
        {
            xCordinate += speed;
            headXBuffer = speed < 0 ? (headXBuffer < xCordinate ? headXBuffer : xCordinate) : (headXBuffer > xCordinate ? headXBuffer : xCordinate);
            if (!this.CanMoveToPosition(nextCell = speed > 0 ? xBuffer + 1 : xCordinate, yCordinate) || !this.CanMoveToPosition(xCordinate, yCordinate))
            {
                throw new InvalidMovementException($"{ship}. Cannot Move to Column: {xCordinate}, row: {yCordinate}");
            }

            this._playingField.OccupyCell(yCordinate, xCordinate, ship);
            this._playingField.CleanUp(xBuffer, yBuffer);
            ship.Tail = speed < 0 ? this._playingField.GetCell(headXBuffer, yCordinate) : this._playingField.GetCell(xCordinate, yCordinate);
            ship.Head = speed < 0 ? this._playingField.GetCell(xCordinate, yCordinate) : this._playingField.GetCell(headXBuffer, yCordinate);
            xBuffer = speed > 0 ? --xBuffer : ++xBuffer;
            xCordinate = xBuffer;
            xCordinateCheck = speed > 0 ? headXBuffer + absoluteSpeed : headXBuffer - absoluteSpeed;
            yCordinateCheck = headYBuffer;
        }

        private void SouthDirection(int speed, Ship ship, ref int yCordinate, int xCordinate, ref int headYBuffer, int headXBuffer, out int xCordinateCheck, out int yCordinateCheck, int absoluteSpeed, ref int yBuffer, int xBuffer, out int nextCell)
        {
            yCordinate -= speed;
            headYBuffer = speed < 0 ? (headYBuffer > yCordinate ? headYBuffer : yCordinate) : (headYBuffer < yCordinate ? headYBuffer : yCordinate);
            if (!this.CanMoveToPosition(xCordinate, nextCell = speed > 0 ? yBuffer - 1 : yCordinate) || !this.CanMoveToPosition(xCordinate, yCordinate))
            {
                throw new InvalidMovementException($"{ship}. Cannot Move to Column: {xCordinate}, row: {yCordinate}");
            }

            this._playingField.OccupyCell(yCordinate, xCordinate, ship);
            this._playingField.CleanUp(xBuffer, yBuffer);
            ship.Tail = speed < 0 ? this._playingField.GetCell(headXBuffer, yCordinate) : this._playingField.GetCell(xCordinate, yCordinate);
            ship.Head = speed < 0 ? this._playingField.GetCell(xCordinate, yCordinate) : this._playingField.GetCell(headXBuffer, yCordinate);
            yBuffer = speed > 0 ? ++yBuffer : --yBuffer;
            yCordinate = yBuffer;
            yCordinateCheck = speed > 0 ? headYBuffer - absoluteSpeed : headYBuffer + absoluteSpeed;
            xCordinateCheck = headXBuffer;
        }

        private void NorthDirection(int speed, Ship ship, ref int yCordinate, int xCordinate, ref int headYBuffer, int headXBuffer, out int xCordinateCheck, out int yCordinateCheck, int absoluteSpeed, ref int yBuffer, int xBuffer, out int nextCell)
        {
            yCordinate += speed;
            headYBuffer = speed < 0 ? (headYBuffer < yCordinate ? headYBuffer : yCordinate) : (headYBuffer > yCordinate ? headYBuffer : yCordinate);
            if (!this.CanMoveToPosition(xCordinate, nextCell = speed > 0 ? yBuffer + 1 : yCordinate) || !this.CanMoveToPosition(xCordinate, yCordinate))
            {
                throw new InvalidMovementException($"{ship}. Cannot Move to Column: {xCordinate}, row: {yCordinate}");
            }

            this._playingField.OccupyCell(yCordinate, xCordinate, ship);
            this._playingField.CleanUp(xBuffer, yBuffer);
            ship.Tail = speed < 0 ? this._playingField.GetCell(xCordinate, headYBuffer) : this._playingField.GetCell(xBuffer, yCordinate);
            ship.Head = speed < 0 ? this._playingField.GetCell(xBuffer, yCordinate) : this._playingField.GetCell(xCordinate, headYBuffer);
            yBuffer = speed > 0 ? --yBuffer : ++yBuffer;
            yCordinate = yBuffer;
            yCordinateCheck = speed > 0 ? headYBuffer + absoluteSpeed : headYBuffer - absoluteSpeed;
            xCordinateCheck = headXBuffer;
        }

        private bool CanMoveToPosition(int xCordinate, int yCordinate)
        {
            return this._playingField.Check(xCordinate, yCordinate);
        }
    }
}
