using System;
using System.Collections.Generic;
using System.IO;

namespace ConsolePaintApp
{
    class FileManager
    {
        public void SaveCanvas(Canvas canvas, string filename)
        {
            try
            {
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
                Console.WriteLine($"Error saving canvas: {ex.Message}");
            }
        }

        public void LoadCanvas(Canvas canvas, string filename)
        {
            try
            {
                if (!File.Exists(filename))
                {
                    Console.WriteLine("Error: File not found.");
                    return;
                }

                using (StreamReader reader = new StreamReader(filename))
                {
                    string[] dimensions = reader.ReadLine().Split(',');
                    int width = int.Parse(dimensions[0]);
                    int height = int.Parse(dimensions[1]);
                    if (width != canvas.Width || height != canvas.Height)
                    {
                        Console.WriteLine("Error: Canvas dimensions do not match.");
                        return;
                    }

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
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading canvas: {ex.Message}");
            }
        }
    }
}