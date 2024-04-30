using BattleShip.Models;

namespace BattleShip.Field
{
    public interface IFieldPlacement
    {
        void AddShipInField(int column, int row, Ship ship, Direction? direction);
    }
}
