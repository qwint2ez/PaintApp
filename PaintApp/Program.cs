using System;

namespace ConsolePaintApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to ConsolePaintApp!");
            Console.Write("Enter canvas width (e.g., 20): ");
            int width = GetValidInput(10, 50);
            Console.Write("Enter canvas height (e.g., 10): ");
            int height = GetValidInput(5, 30);

            Canvas canvas = new Canvas(width, height);
            Command commandHandler = new Command(canvas);

            commandHandler.DisplayHelp();
            while (true)
            {
                Console.Write("\nEnter command: ");
                string command = Console.ReadLine();
                commandHandler.Execute(command);
            }
        }

        static int GetValidInput(int min, int max)
        {
            while (true)
            {
                if (int.TryParse(Console.ReadLine(), out int value) && value >= min && value <= max)
                    return value;
                Console.Write($"Please enter a number between {min} and {max}: ");
            }
        }
    }
}