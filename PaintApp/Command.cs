using System;

namespace ConsolePaintApp
{
    class Command
    {
        private Canvas canvas;

        public Command(Canvas canvas)
        {
            this.canvas = canvas;
        }

        public void Execute(string command)
        {
            switch (command?.ToLower().Trim())
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
            try
            {
                Console.Write("Enter shape type (circle/square): ");
                string type = Console.ReadLine()?.ToLower();
                Console.Write("Enter X: ");
                int x = int.Parse(Console.ReadLine());
                Console.Write("Enter Y: ");
                int y = int.Parse(Console.ReadLine());
                Console.Write("Enter size: ");
                int size = int.Parse(Console.ReadLine());

                Shape shape = type switch
                {
                    "circle" => new Circle(x, y, size),
                    "square" => new Square(x, y, size),
                    _ => throw new Exception("Invalid shape type.")
                };
                canvas.AddShape(shape);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        private void EraseShape()
        {
            try
            {
                canvas.DisplayShapes();
                Console.Write("Enter index to erase: ");
                int index = int.Parse(Console.ReadLine());
                canvas.EraseShape(index);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        private void MoveShape()
        {
            try
            {
                canvas.DisplayShapes();
                Console.Write("Enter index to move: ");
                int index = int.Parse(Console.ReadLine());
                Console.Write("Enter new X: ");
                int newX = int.Parse(Console.ReadLine());
                Console.Write("Enter new Y: ");
                int newY = int.Parse(Console.ReadLine());
                canvas.MoveShape(index, newX, newY);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        private void SetBackground()
        {
            try
            {
                canvas.DisplayShapes();
                Console.Write("Enter index to set background: ");
                int index = int.Parse(Console.ReadLine());
                Console.Write("Enter background character: ");
                char bgChar = Console.ReadLine()[0];
                canvas.SetBackground(index, bgChar);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        private void SaveCanvas()
        {
            try
            {
                Console.Write("Enter filename to save: ");
                string filename = Console.ReadLine();
                FileManager fileManager = new FileManager();
                fileManager.SaveCanvas(canvas, filename);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        private void LoadCanvas()
        {
            try
            {
                Console.Write("Enter filename to load: ");
                string filename = Console.ReadLine();
                FileManager fileManager = new FileManager();
                fileManager.LoadCanvas(canvas, filename);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }
}   