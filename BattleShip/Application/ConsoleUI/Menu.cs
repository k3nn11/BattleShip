using System;

namespace Application.ConsoleUI
{
    public class Menu
    {
        private string[] _options;

        private int _selectedOption;

        private string _prompt;

        public Menu(string[] options, string prompt)
        {
            this._options = options;
            this._selectedOption = 0;
            this._prompt = prompt;
        }

        public int Run()
        {
            ConsoleKey keyPressed;
            do
            {
                Console.Clear();
                this.DisplayOptions();
                ConsoleKeyInfo consoleKeyInfo = Console.ReadKey(true);
                keyPressed = consoleKeyInfo.Key;
                if (keyPressed == ConsoleKey.UpArrow)
                {
                    this._selectedOption--;
                    if (this._selectedOption == -1)
                    {
                        this._selectedOption = this._options.Length - 1;
                    }
                }
                else if (keyPressed == ConsoleKey.DownArrow)
                {
                    this._selectedOption++;
                    if (this._selectedOption == this._options.Length)
                    {
                        this._selectedOption = 0;
                    }
                }
            }
            while (keyPressed != ConsoleKey.Enter);
            return this._selectedOption;
        }

        private void DisplayOptions()
        {
            Console.WriteLine(this._prompt);
            string prefix = string.Empty;
            for (int i = 0; i < this._options.Length; i++)
            {
                var option = this._options[i];
                if (i == this._selectedOption)
                {
                    prefix = "#";
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.BackgroundColor = ConsoleColor.Red;
                }
                else
                {
                    prefix = " ";
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.BackgroundColor = ConsoleColor.Black;
                }

                Console.WriteLine($"{prefix}<<{option}>>");
            }

            Console.ResetColor();
        }
    }
}
