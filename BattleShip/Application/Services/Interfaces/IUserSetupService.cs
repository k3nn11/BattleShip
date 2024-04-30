using BattleShip.Field;
using BattleShip.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Services
{
    public interface IUserSetupService
    {
        Task<User> SetUser(int id);

        Task<PlayingField> AddExistingPlayingField(int id);

        void AddNewUserPlayingField(PlayingField key);

        Task<Dictionary<PlayingField, List<Ship>>> GetUserPlayingFields();

        Task<PlayingField> AddExistingShip(int shipId, int fieldId);

        Task<PlayingField> AddNewShip(int fieldId, Ship ship);
    }
}
