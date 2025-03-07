using System;

namespace ConsolePaintApp
{
    public class Command
    {
        private Canvas canvas;

        public Command(Canvas canvas)
        {
            this.canvas = canvas;
        }

        public void Execute(string command)
        {
            if (string.IsNullOrEmpty(command))
            {
                Console.WriteLine("Error: Command cannot be empty.");
                return;
            }

            string cmd = command.ToLower().Trim();
            switch (cmd)
            {
                case "add":
                    AddShape();
                    break;
                case "erase":
                    EraseShape();
                    break;
                case "move":
                    MoveShape();
                    break;
                case "background":
                    SetBackground();
                    break;
                case "save":
                    SaveCanvas();
                    break;
                case "load":
                    LoadCanvas();
                    break;
                case "undo":
                    canvas.Undo();
                    break;
                case "redo":
                    canvas.Redo();
                    break;
                case "list":
                    canvas.DisplayShapes();
                    break;
                case "exit":
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Unknown command.");
                    break;
            }
        }

        private void AddShape()
        {
            Console.Write("Enter shape type (circle/square/triangle): ");
            string type = Console.ReadLine()?.ToLower();
            if (type != "circle" && type != "square" && type != "triangle")
            {
                Console.WriteLine("Error: Invalid shape type.");
                return;
            }

            Console.Write("Enter X: ");
            if (!int.TryParse(Console.ReadLine(), out int x) || x < 0)
            {
                Console.WriteLine("Error: X must be a non-negative integer.");
                return;
            }

            Console.Write("Enter Y: ");
            if (!int.TryParse(Console.ReadLine(), out int y) || y < 0)
            {
                Console.WriteLine("Error: Y must be a non-negative integer.");
                return;
            }

            Console.Write("Enter size: ");
            if (!int.TryParse(Console.ReadLine(), out int size) || size <= 0)
            {
                Console.WriteLine("Error: Size must be a positive integer.");
                return;
            }

            Shape shape = type switch
            {
                "circle" => new Circle(x, y, size),
                "square" => new Square(x, y, size),
                "triangle" => new Triangle(x, y, size),
                _ => throw new Exception("Unknown shape type.")
            };
            canvas.AddShape(shape);
        }

        private void EraseShape()
        {
            Console.Write("Enter index to erase: ");
            if (!int.TryParse(Console.ReadLine(), out int index) || index < 0)
            {
                Console.WriteLine("Error: Index must be a non-negative integer.");
                return;
            }
            canvas.EraseShape(index);
        }

        private void MoveShape()
        {
            Console.Write("Enter index to move: ");
            if (!int.TryParse(Console.ReadLine(), out int index) || index < 0)
            {
                Console.WriteLine("Error: Index must be a non-negative integer.");
                return;
            }

            Console.Write("Enter new X: ");
            if (!int.TryParse(Console.ReadLine(), out int newX) || newX < 0)
            {
                Console.WriteLine("Error: New X must be a non-negative integer.");
                return;
            }

            Console.Write("Enter new Y: ");
            if (!int.TryParse(Console.ReadLine(), out int newY) || newY < 0)
            {
                Console.WriteLine("Error: New Y must be a non-negative integer.");
                return;
            }

            canvas.MoveShape(index, newX, newY);
        }

        private void SetBackground()
        {
            Console.Write("Enter index to set background: ");
            if (!int.TryParse(Console.ReadLine(), out int index) || index < 0)
            {
                Console.WriteLine("Error: Index must be a non-negative integer.");
                return;
            }

            Console.Write("Enter background character: ");
            string input = Console.ReadLine();
            if (string.IsNullOrEmpty(input))
            {
                Console.WriteLine("Error: Background character cannot be empty.");
                return;
            }
            char bgChar = input[0];
            canvas.SetBackground(index, bgChar);
        }

        private void SaveCanvas()
        {
            Console.Write("Enter filename to save: ");
            string filename = Console.ReadLine();
            if (string.IsNullOrEmpty(filename))
            {
                Console.WriteLine("Error: Filename cannot be empty.");
                return;
            }
            FileManager fileManager = new FileManager();
            fileManager.SaveCanvas(canvas, filename);
        }

        private void LoadCanvas()
        {
            Console.Write("Enter filename to load: ");
            string filename = Console.ReadLine();
            if (string.IsNullOrEmpty(filename))
            {
                Console.WriteLine("Error: Filename cannot be empty.");
                return;
            }
            FileManager fileManager = new FileManager();
            fileManager.LoadCanvas(canvas, filename);
        }
    }
}