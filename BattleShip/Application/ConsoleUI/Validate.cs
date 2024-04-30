using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ConsoleUI
{
    public class Validate
    {
        public static int Interger()
        {
            while (true)
            {
                string userInput = Console.ReadLine();

                if (int.TryParse(userInput, out int number))
                {
                    return number;
                }
                else
                {
                    Console.WriteLine("Invalid integer number\nEnter again");
                    continue;
                }
            }
        }

        public static char Choice(string prompt)
        {
            Console.WriteLine($"{prompt}: Y/N");
            while (true)
            {
                bool isValid = char.TryParse(Console.ReadLine().ToUpper(), out char input);
                if (isValid)
                {
                    if (input != 'Y' && input != 'N')
                    {
                        Console.WriteLine("Enter valid input Y/N");
                        continue;
                    }
                    else if (input == 'Y')
                    {
                        return input;
                    }
                    else if (input == 'N')
                    {
                        return input;
                    }
                }
                else
                {
                    Console.WriteLine("Enter valid input: Y/N");
                    continue;
                }
            }
        }
    }
}
