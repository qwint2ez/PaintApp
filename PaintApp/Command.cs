using System;
using System.IO;

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
            DisplayHelp(); // Показываем команды после каждого действия
        }

        private void AddShape()
        {
            try
            {
                Console.Write("Enter shape type (circle/square): ");
                string type = Console.ReadLine()?.ToLower();
                Console.Write("Enter X position: ");
                int x = int.Parse(Console.ReadLine());
                Console.Write("Enter Y position: ");
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
                Console.Write("Enter new X position: ");
                int newX = int.Parse(Console.ReadLine());
                Console.Write("Enter new Y position: ");
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
                using (StreamWriter writer = new StreamWriter(filename))
                {
                    writer.WriteLine($"{canvas.Width},{canvas.Height}");
                    foreach (var shape in canvas.GetShapes())
                    {
                        string type = shape.GetType().Name;
                        writer.WriteLine($"{type},{shape.X},{shape.Y},{shape.Size},{shape.FillChar},{shape.BackgroundChar}");
                    }
                }
                Console.WriteLine("Canvas saved successfully.");
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
                if (File.Exists(filename))
                {
                    using (StreamReader reader = new StreamReader(filename))
                    {
                        string[] dimensions = reader.ReadLine().Split(',');
                        int width = int.Parse(dimensions[0]);
                        int height = int.Parse(dimensions[1]);
                        if (width != canvas.Width || height != canvas.Height)
                            throw new Exception("Canvas dimensions do not match.");

                        canvas.GetShapes().Clear();
                        string line;
                        while ((line = reader.ReadLine()) != null)
                        {
                            string[] parts = line.Split(',');
                            string type = parts[0];
                            int x = int.Parse(parts[1]);
                            int y = int.Parse(parts[2]);
                            int size = int.Parse(parts[3]);
                            char fillChar = parts[4][0];
                            char bgChar = parts[5][0];

                            Shape shape = type switch
                            {
                                "Circle" => new Circle(x, y, size),
                                "Square" => new Square(x, y, size),
                                _ => throw new Exception("Unknown shape type.")
                            };
                            shape.FillChar = fillChar;
                            shape.BackgroundChar = bgChar;
                            canvas.AddShape(shape);
                        }
                    }
                    Console.WriteLine("Canvas loaded successfully.");
                }
                else
                {
                    Console.WriteLine("Error: File not found.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        public void DisplayHelp()
        {
            Console.WriteLine("\nAvailable commands: add, erase, move, background, save, load, undo, redo, list, exit");
        }
    }
}