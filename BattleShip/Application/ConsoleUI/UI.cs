using System;
using System.Linq;
using Application.ConsoleUI;
using BattleShip.Field;
using BattleShip.Models;
using Application.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.ConsoleUI
{
    public class UI
    {
        private static IPlayingFieldService _fieldManager;

        private static User _user;

        private static IUserService _userManager;

        public UI(IPlayingFieldService fieldService, IUserService userService)
        {
            _fieldManager = fieldService;
            _userManager = userService;
        }

        public static void RunMainMenu()
        {
            string prompt = "Welcome to Battle Ship game. What would you like  to do?";
            string[] options = { "Start", "About", "Exit" };
            Menu gameMenu = new Menu(options, prompt);
            int selectedIndex = gameMenu.Run();

            switch (selectedIndex)
            {
                case 0:
                    RunStartMenu();
                    break;
                case 1:
                    About();
                    break;
                case 2:
                    Exit();
                    break;
            }
        }

        private static void RunStartMenu()
        {
            Console.Clear();
            Start();
        }

        private async static void RunPlayMenu(Action action)
        {
            Console.Clear();
            _user = await SelectUser();
            ChooseField();
            Back(action);
        }

        private static void RunUserMenu()
        {
            Console.Clear();
            Users();
        }

        private static void Start()
        {
            string prompt = "Battle Ship Start Menu";
            string[] options = { "Create Playing Field", "Play", "Users", "Back" };
            Menu startmenu = new Menu(options, prompt);
            int selectedOption = startmenu.Run();
            switch (selectedOption)
            {
                case 0:
                    CreatePlayingField(Start);
                    break;
                case 1:
                    RunPlayMenu(Play);
                    break;
                case 2:
                    RunUserMenu();
                    break;
                case 3:
                    Back(RunMainMenu);
                    break;
            }
        }

        private static void Play()
        {
            string prompt = "Welcome to the BattleShip game. Get started by adding your ships into your playingFields";
            string[] options = { "Add Ship", "Movement", "Shoot", "Repair", "Compare Ship", "Field State", "Back" };
            Menu playMenu = new Menu(options, prompt);
            int selectedOption = playMenu.Run();
            switch (selectedOption)
            {
                case 0:
                    AddShipInField();
                    break;
                case 1:
                    Sail();
                    break;
                case 2:
                    Shoot();
                    break;
                case 3:
                    Repair();
                    break;
                case 4:
                    CompareShip();
                    break;
                case 5:
                    FieldStateInPlay();
                    break;
                case 6:
                    Back(Start);
                    break;
            }

            Console.ReadKey();
        }

        private async static void ChooseField()
        {
            var fields = await _fieldManager.GetFields();
            if (fields.Count == 0)
            {
                Console.WriteLine("There are no playingFields available.");
                Console.WriteLine("Press any key to Create a playing Field");
                Console.ReadKey();
                RunStartMenu();
            }

            string prompt = "Please choose the Playing fields to play in";
            string[] options = { "All Playing Fields", "User Playing Fields" };
            Menu menu = new Menu(options, prompt);
            int selectedOption = menu.Run();
            switch (selectedOption)
            {
                case 0:
                    _user.PlayingField =await GetPlayingFieldAsync();
                    _user.AddOrUpdate(_user.PlayingField, _user.Ships = new List<Ship>());
                    break;
                case 1:
                    if (_user.FieldShipPairs.Keys.Count == 0)
                    {
                        Console.WriteLine("User has no PlayingFields Currently. Press any Key to Add Playing Field");
                        Console.ReadKey();
                        _user.PlayingField =await GetPlayingFieldAsync();
                        _user.AddOrUpdate(_user.PlayingField, _user.Ships = new List<Ship>());
                    }
                    else
                    {
                        _user.PlayingField = GetUSerField();
                    }

                    break;
            }
        }

        private static void Exit()
        {
            Console.Clear();
            Console.WriteLine("Press any Key to exit..");
            Console.ReadKey(true);
            Environment.Exit(0);
        }

        private static void About()
        {
            Console.Clear();
            Console.WriteLine("This Battleship board game is an aspiration from the classic game of naval combat that brings together competition," +
                " strategy, and excitement!\nIn head-to-head battle, 2 players search for the enemy's fleet of ships and destroys them one by one.\n");
            Console.WriteLine("The game has three types of ships namely: Military, Auxilliary and Mixed ships.");
            Console.WriteLine("Military Ships have Weapons and can shoot other ships\n"
                + "Auxillairy Ships are responsible for repairing the damaged ships\n"
                + "Mixed ships are can shoot and repair other ships\n");
            Console.WriteLine($"Created on:  {new DateTime(2024, 03, 07)}\n");
            Console.WriteLine("Creator : Kennedy Githua.");
            Console.ReadKey(true);
            RunMainMenu();
        }

        private static void Back(Action action)
        {
            action();
        }

        private static void Users()
        {
            string prompt = "Choose an action";
            string[] options = { "Add", "Remove", "Remove all", "Back" };
            Menu menu = new Menu(options, prompt);
            int selectedOption = menu.Run();
            switch (selectedOption)
            {
                case 0:
                    AddUser();
                    break;
                case 1:
                    RemoveUSer();
                    break;
                case 2:
                    RemoveAllUsers();
                    break;
                case 3:
                    RunStartMenu();
                    break;
            }
        }

        private static void AddUser()
        {
            Console.Clear();
            Console.WriteLine("Create new User");
            Console.WriteLine("Enter name: ");
            var name = Console.ReadLine();
            Console.WriteLine($"User name: {name}\n Press and key to continue");
            Console.ReadKey();
            User user = new User();
            user.FirstName = name;
            _userManager.AddUser(user);
            RunUserMenu();
        }

        private async static void RemoveUSer()
        {
            Console.Clear();
            int index = 0;
            var users = await _userManager.GetUsers();
            if (users.Count == 0)
            {
                Console.WriteLine("No users Available");
                Console.ReadKey();
            }
            else
            {
                foreach (var user in users)
                {
                    Console.WriteLine("{0}: {1}", index++, user);
                }

                Console.WriteLine("Enter an Index to delete user: ");
                int userIndex = Validate.Interger();
                var user1 = await _userManager.GetUserByIdAsync(userIndex);
                _userManager.RemoveUser(user1);
                Console.WriteLine("DONE!");
                Console.ReadKey();
            }

            RunUserMenu();
        }

        private async static Task RemoveAllUsers()
        {
            Console.Clear();
            var users = await _userManager.GetUsers();
            if (users.Count == 0)
            {
                Console.WriteLine("No users Available");
                Console.ReadKey();
            }
            else
            {
                string prompt = "Confirm to delete all users";
                var confirm = Validate.Choice(prompt);
                if (confirm == 'Y')
                {
                    _userManager.RemoveAll();
                }

                Console.WriteLine("DONE!");
                Console.ReadKey();
            }

            RunUserMenu();
        }

        private async static Task<User> SelectUser()
        {
            int index = 0;
            var users = await _userManager.GetUsers();
            if (users.Count == 0)
            {
                Console.WriteLine("No Users found.Please create User to continue. Press any key to continue");
                Console.ReadKey();
                RunUserMenu();
            }

            string[] options = new string[users.Count];
            users.ForEach(x =>
            options[index++] = x.FirstName);

            string prompt = "Select User to play";
            Menu menu = new Menu(options, prompt);
            int selectedPlayer = menu.Run();
            return await _userManager.GetUserByIdAsync(selectedPlayer);
        }

        private static void CreatePlayingField(Action action)
        {
            Console.Clear();
            bool isCreated;
            PlayingField playingField = null;
            Console.WriteLine("Provide a name for your PlayingField: ");
            var name = Console.ReadLine();
            Console.WriteLine("Please provide the dimensions of your playing field. KINDLY NOTE, PlayingField dimensions should be odd numbers.");
            int height = 0;
            int width = 0;
            var total = 0;
            do
            {
                try
                {
                    isCreated = true;
                    Console.Write("Enter Height: ");
                    height = Validate.Interger();
                    Console.Write("Enter Width: ");
                    width = Validate.Interger();
                    total = height * width;
                    playingField = new PlayingField(height, width);
                    playingField.Name = name;
                    Console.WriteLine($"\nField Created - {playingField}");
                    playingField.DisplayField();
                    _fieldManager.AddField(playingField);
                }
                catch (ArgumentException e)
                {
                    Console.WriteLine(e.Message);
                    isCreated = false;
                }
            }
            while (!isCreated);
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey(true);
            Back(action);
        }

        private static Ship CreateShip()
        {
            string shipPrompt = "Please select a type of a ship to build";
            string[] shipOptions = { "Military", "Mixed", "Auxilliary", "Back" };
            Menu shipMenu = new Menu(shipOptions, shipPrompt);
            int selectedOption = shipMenu.Run();
            int length = 0;
            int speed = 0;
            Ship ship = null;
            switch (selectedOption)
            {
                case 0:
                    SetShipParameters(out length, out speed);
                    ship = new Military(length, speed);
                    break;
                case 1:
                    SetShipParameters(out length, out speed);
                    ship = new Mixed(length, speed);
                    break;
                case 2:
                    SetShipParameters(out length, out speed);
                    ship = new Auxilliary(length, speed);
                    break;
                case 3:
                    Back(Play);
                    break;
            }

            _user.Ships.Add(ship);
            return ship;
        }

        private static void SetShipParameters(out int length, out int speed)
        {
            Console.Clear();
            do
            {
                Console.WriteLine("Provide details of ship");
                Console.Write("Enter length: ");
                length = Validate.Interger();
                Console.Write("Enter Speed: ");
                speed = Validate.Interger();
                if (!(length > 0) && (speed > 0))
                {
                    Console.WriteLine($"Speed and Length have to be greater than zero");
                }
            }
            while (!(length > 0) && (speed > 0));
        }

        private static void FieldStateInPlay()
        {
            FieldState();
            Play();
        }

        private static void FieldState()
        {
            Console.Clear();
            var index = 1;
            _user.PlayingField.DisplayField();
            if (_user.PlayingField.Ships.Count == 0)
            {
                Console.WriteLine("There are {0} added ships in the playing Field", _user.PlayingField.Ships.Count);
            }
            else
            {
                Console.WriteLine("There are {0} added ships in the Playing Field", _user.PlayingField.Ships.Count);
                if (_user.FieldShipPairs[_user.PlayingField].Count == 0)
                {
                    Console.WriteLine("User has {0} added ships in the Playing Field", _user.FieldShipPairs[_user.PlayingField].Count);
                }
                else
                {
                    _user.FieldShipPairs[_user.PlayingField].ForEach(ship => Console.WriteLine($"Index: {index++}. {ship}"));
                }
            }

            Console.WriteLine("Press any key to continue");
            Console.ReadKey();
        }

        private static void AddShipInField()
        {
            ConsoleKey keyPressed;
            bool isCreated = true;
            do
            {
                Console.Clear();
                try
                {
                    Console.WriteLine("Complete the next steps to add ship into the playing Field. Press Enter to continue or Escape to go back");
                    ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                    keyPressed = keyInfo.Key;
                    if (keyPressed != ConsoleKey.Enter)
                    {
                        if (keyPressed == ConsoleKey.Escape)
                        {
                            Back(Play);
                        }

                        isCreated = false;
                        continue;
                    }

                    Ship ship = CreateShip();
                    _user.PlayingField.DisplayField();
                    Console.WriteLine("\nPlease specify coordinates for your ship\n");
                    Console.WriteLine("Enter column:");
                    int column = Validate.Interger();
                    Console.WriteLine("Enter row:");
                    int row = Validate.Interger();
                    ShipDirectionOption();
                    Func<Direction?> getDirection = GetDirection();
                    Direction? direction = getDirection();
                    Console.WriteLine($"Your Specifications: Cordinates: {column}, {row}, Ship: {ship}, Direction: {direction}");
                    _user.PlayingField.AdjustCordinate(ref column, ref row);
                    _user.PlayingField.FieldPlacement.AddShipInField(column, row, ship, direction);
                    isCreated = true;
                    _user.FieldShipPairs[_user.PlayingField].Add(ship);
                }
                catch (IndexOutOfRangeException e)
                {
                    Console.WriteLine(e.Message);
                    isCreated = false;
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    isCreated = false;
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                }
            }
            while (!isCreated);
            Console.ReadKey(true);
            Back(Play);
        }

        private static Func<Direction?> GetDirection()
        {
            return () =>
            {
                var choice = Validate.Interger();
                switch (choice)
                {
                    case 1:
                        return Direction.North;
                    case 2:
                        return Direction.South;
                    case 3:
                        return Direction.West;
                    case 4:
                        return Direction.East;
                    default:
                        return null;
                }
            };
        }

        private static async Task<PlayingField> GetPlayingFieldAsync()
        {
            Console.Clear();
            var fields = await _fieldManager.GetFields();
            if (fields.Count == 0)
            {
                return null;
            }

            bool isCompleted = false;
            PlayingField field = null;
            int choice = 0;
            do
            {
                int index = 0;
                try
                {
                    fields.ForEach(pf =>
                    {
                        Console.WriteLine($"Index: {index}. {pf}");
                        pf.DisplayField();
                        index++;
                    });
                    Console.WriteLine("Please enter an index to select a Playing Field");
                    choice = Validate.Interger();
                    Console.WriteLine($"Your PlayingField Choice: {_fieldManager.GetFieldById(choice)}");
                    field = await _fieldManager.GetFieldById(choice);
                    isCompleted = true;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                }
            }
            while (!isCompleted);
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
            return field;
        }

        private static PlayingField GetUSerField()
        {
            if (_user == null)
            {
                return null;
            }

            int choice;
            bool isComplete = false;
            PlayingField playingField = null;
            var playingFields = _user.FieldShipPairs.Keys;
            if (playingFields.Count == 0)
            {
                return null;
            }

            do
            {
                int index = 0;
                try
                {
                    foreach (var field in playingFields)
                    {
                        Console.WriteLine($"Index: {index++}- {field}");
                        field.DisplayField();
                    }

                    Console.WriteLine("Please enter an index to select a Playing Field");
                    choice = Validate.Interger();
                    Console.WriteLine($"Your PlayingField Choice: {playingFields.ElementAt(choice)}");
                    playingFields.ElementAt(choice).DisplayField();
                    playingField = playingFields.ElementAt(choice);
                    isComplete = true;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    Console.ReadKey();
                }
            }
            while (!isComplete);
            return playingField;
        }

        private static Ship GetShip()
        {
            var ships = _user.FieldShipPairs[_user.PlayingField];
            if (ships == null || ships.Count == 0)
            {
                return null;
            }

            FieldState();
            Console.WriteLine();
            Console.WriteLine("Please enter an index to select a ship");
            int choice = Validate.Interger();
            choice--;
            Console.WriteLine($"Your ship Choice: {ships[choice]}");
            Console.ReadKey();
            return ships[choice];
        }

        private static void ShipDirectionOption()
        {
            Console.WriteLine("Choose Direction");
            Console.WriteLine("1: North\n2: South\n3: West\n4: East");
        }

        private static void RunSailMenu()
        {
            Console.Clear();
            FieldState();
            Sail();
        }

        private static void Sail()
        {
            Console.Clear();
            string prompt = "Please select a movement for the ship";
            string[] options = { "Forward", "Reverse", "Back" };
            Menu sailMenu = new Menu(options, prompt);
            int selectedOption = sailMenu.Run();
            switch (selectedOption)
            {
                case 0:
                    Forward();
                    break;
                case 1:
                    Reverse();
                    break;
                case 2:
                    Back(Play);
                    break;
            }
        }

        private static void Forward()
        {
            bool isCompleted = false;
            do
            {
                try
                {
                    Console.Clear();
                    Ship ship = GetShip();
                    if (ship == null)
                    {
                        isCompleted = true;
                    }
                    else
                    {
                        _user.PlayingField.Movement.Forward(ship);
                        isCompleted = true;
                        Console.ReadKey();
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    Console.ReadKey();
                    RunSailMenu();
                }
            }
            while (!isCompleted);
            RunSailMenu();
        }

        private static void Reverse()
        {
            bool isCompleted = false;
            do
            {
                try
                {
                    Console.Clear();
                    Ship ship = GetShip();
                    if (ship == null)
                    {
                        isCompleted = true;
                    }
                    else
                    {
                        _user.PlayingField.Movement.Reverse(ship);
                        isCompleted = true;
                        Console.ReadKey();
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    Console.ReadKey();
                    RunSailMenu();
                }
            }
            while (!isCompleted);
            RunSailMenu();
        }

        private static void Shoot()
        {
            Console.Clear();
            Ship ship = GetShip();
            Console.WriteLine("NOTE!! Only Military and Mixed Ships can shoot");
            Console.WriteLine($"Please enter the coordinates you would like to shoot. Cordinates are non negative and greater than zero. The maximum range for column:" +
                $" {_user.PlayingField.Width} and row: {_user.PlayingField.Height}");
            Console.WriteLine("Enter Column:");
            int column = Validate.Interger();
            Console.WriteLine("Enter Row: ");
            int row = Validate.Interger();
            var typeName = ship.GetType().Name;
            if (typeName == "Military" || typeName == "Mixed")
            {
                Military military = ship as Military;
                _user.PlayingField.AdjustCordinate(ref column, ref row);
                military.Weapon.ShootImplementation(_user.PlayingField, column, row);
            }
            else
            {
                Console.WriteLine("The Ship cannot shoot.");
            }

            FieldState();
            Back(Play);
        }

        private static void Repair()
        {
        }

        private static void CompareShip()
        {
        }
    }
}
