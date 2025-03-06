using System;

namespace ConsolePaintApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to ConsolePaintApp!");
            Canvas canvas = new Canvas();
            Command commandHandler = new Command(canvas);

            while (true)
            {
                canvas.Redraw();
                Console.WriteLine("\nAvailable commands: add, erase, move, background, save, load, undo, redo, list, exit");
                Console.Write("Enter command: ");
                string command = Console.ReadLine();
                commandHandler.Execute(command);
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
            }
        }
    }
}