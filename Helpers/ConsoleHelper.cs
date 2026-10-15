using System;
using System.Collections.Generic;
using System.Text;
using To_Do_CLI.Enum;

namespace To_Do_CLI.Helpers
{
    public static class ConsoleHelper
    {
        public static string ReadString(string promt, bool allowEmpty = false)
        {
            Console.WriteLine(promt);
            string input = Console.ReadLine();
            if(!allowEmpty && string.IsNullOrEmpty(input))
            {
                Console.WriteLine("Field cannot be empty!");
                ReadString(promt, allowEmpty);
            }
            return input;
        }

        public static int ReadInt (string promt, int min=0, int max= int.MaxValue)
        {
            Console.WriteLine(promt);
            string input = Console.ReadLine();
            if (!int.TryParse(input, out int result))
            {
                Console.WriteLine("Enter an integer");
                ReadInt(promt, min, max);
            }

            if (result < min || result> max)
            {
                Console.WriteLine($"Enter a number from {min} to {max}!");
                ReadInt(promt, min, max);
            }
            return result;
        }

        public static TaskPriority ReadPriority(string promt="Choose priority where 1 - Low, 2 - Medium, 3 - High")
        {
            int choice = ReadInt(promt, 1, 3);
            return (TaskPriority)choice;
        }

        public static void WriteLineColored (string message, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(message);
            Console.ResetColor();
        }

        public static void PrintHeader(string title, char border = '=')
        {
            int width = 50;
            Console.WriteLine(new string(border, width));
            Console.WriteLine(" "+title);
            Console.WriteLine(new string(border, width));
        }

        public static void WaitForKey(string message = "Press any key to continue")
        {
            Console.WriteLine($"\n {message}");
            Console.ReadKey();
        }
    }
}
