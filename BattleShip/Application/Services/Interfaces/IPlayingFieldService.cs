using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BattleShip.Field;

namespace Application.Services
{
    public interface IPlayingFieldService
    {
        void AddField(PlayingField field);

        void RemoveField(PlayingField field);

        void UpdateField(int id, PlayingField field);

        void RemoveAll();

        Task<PlayingField> GetFieldById(int id);

        Task<List<PlayingField>> GetFields();
    }
}
