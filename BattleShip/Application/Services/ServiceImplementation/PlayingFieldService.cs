using System.Collections.Generic;
using System.Threading.Tasks;
using BattleShip.Field;
using DAL.InMemoryDB;

namespace Application.Services
{
    public class  PlayingFieldService : IPlayingFieldService
    {
        private IRepository<PlayingField> _baseService;

        private int index {  get; set; }

        public PlayingFieldService(IRepository<PlayingField> baseService)
        {
            this._baseService = baseService;
        }

        public void AddField(PlayingField field)
        {
            field.Id = index++;
           this._baseService.Add(field);
        }

        public void RemoveField(PlayingField field)
        {
           this._baseService.Remove(field);
        }

        public void RemoveAll()
        {
            this._baseService.RemoveAll();
        }

        public async Task<PlayingField> GetFieldById(int id)
        {
            var field = await _baseService.GetById(id);
            return field = field.Id == id ? field : null;
        }

        public async Task<List<PlayingField>> GetFields()
        {
            return await this._baseService.GetAll();
        }

        public void UpdateField(int id, PlayingField obj)
        {
            this._baseService.Update(id, obj);
        }
    }
}
