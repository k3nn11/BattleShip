using BattleShip.Field;
using BattleShip.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Services
{
    public class UserSetupService : IUserSetupService
    {
        private IUserService _userService;

        private IPlayingFieldService _fieldService;

        private IShipService _shipService;

        private User _user;

        public UserSetupService(IUserService userService, IShipService shipService, IPlayingFieldService fieldService)
        {
            _shipService = shipService;
            _userService = userService;
            _fieldService = fieldService;
        }

        public async Task<PlayingField> AddExistingPlayingField(int id)
        {
            if (_user == null )
            {
                return null;
            }

             var field = await _fieldService.GetFieldById(id);
            return  _user.UserRepository.AddExistingKey(field);
        }

        public async Task<PlayingField> AddExistingShip(int shipId, int fieldId)
        {
            var field = await GetUserFieldById(fieldId);
            var ship = await _shipService.GetShipByIdAsync(shipId);

            if (_user == null || field == null || ship == null)
            {
                return null;
            }

            _user.UserRepository.AddExistingValue(ship, field);
            field.Ships.Add(ship);
            _fieldService.UpdateField(fieldId, field);
            return field;
        }

        public async Task<PlayingField> AddNewShip(int fieldId, Ship ship)
        {
            var field = await GetUserFieldById(fieldId);
            if (_user == null || field == null)
            {
                return null;
            }

            _user.UserRepository.AddNewValue(field, ship);
            field.Ships.Add(ship);
            _fieldService.UpdateField(fieldId, field);
            return field;
        }

        public void AddNewUserPlayingField(PlayingField field)
        {
            if (_user == null)
            {
                throw new NullReferenceException("User is not set");
            }
            _fieldService.AddField(field);
            _user.UserRepository.AddNewKey(field);
        }

        public async Task<User> SetUser(int id)
        {
            _user = await _userService.GetUserByIdAsync(id);
            return _user = _user != null ? _user : null;
        }

        public async Task<Dictionary<PlayingField, List<Ship>>> GetUserPlayingFields()
        {
            if (_user == null)
            {
                return null;
            }

            return await _user.UserRepository.GetAll();
        }

        public async Task<PlayingField> GetUserFieldById(int id)
        {
            if (_user == null)
            {
                return null;
            }
            var fields = await GetUserPlayingFields();
            foreach (var field in fields)
            {
               if(field.Key.Id == id)
                {
                    return field.Key;
                }          
            }
            return null;
        }
    }
}
