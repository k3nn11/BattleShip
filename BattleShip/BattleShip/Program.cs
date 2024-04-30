using BattleShip.Field;
using BattleShip.Models;
using DAL.InMemoryDB;

namespace BattleShip
{
    public class Program
    {
        public static void Main(string[] args)
        {
            IRepository<User> user = new Repository<User>();
            IRepository<PlayingField> field = new Repository<PlayingField>();
            //IUserService userService = new UserService(user);
            //IPlayingFieldService fieldService = new PlayingFieldService(field);
            //var ui = new UI(fieldService, userService);
            //UI.RunMainMenu();
        }
    }
}