using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsolePaintApp
{
    public class Canvas
    {
        private List<Shape> shapes = new List<Shape>();
        private Stack<List<Shape>> undoStack = new Stack<List<Shape>>();
        private Stack<List<Shape>> redoStack = new Stack<List<Shape>>();
        public const int Width = 40;
        public const int Height = 20;

        public Canvas()
        {

        }

        public void AddShape(Shape shape)
        {
            if (IsWithinBounds(shape))
            {
                SaveStateForUndo();
                shapes.Add(shape);
                Redraw();
            }
            else
            {
                Console.WriteLine("Error: Shape is out of canvas bounds.");
            }
        }

        public void EraseShape(int index)
        {
            if (index >= 0 && index < shapes.Count)
            {
                SaveStateForUndo();
                shapes.RemoveAt(index);
                Redraw();
            }
            else
            {
                Console.WriteLine("Error: Invalid shape index.");
            }
        }

        public void MoveShape(int index, int newX, int newY)
        {
            if (index >= 0 && index < shapes.Count)
            {
                Shape currentShape = shapes[index];
                Shape tempShape = currentShape is Circle ? new Circle(newX, newY, currentShape.Size) : new Square(newX, newY, currentShape.Size);
                if (IsWithinBounds(tempShape))
                {
                    SaveStateForUndo();
                    currentShape.Move(newX, newY);
                    Redraw();
                }
                else
                {
                    Console.WriteLine("Error: Cannot move shape out of bounds.");
                }
            }
            else
            {
                Console.WriteLine("Error: Invalid shape index.");
            }
        }

        public void SetBackground(int index, char bgChar)
        {
            if (index >= 0 && index < shapes.Count)
            {
                SaveStateForUndo();
                shapes[index].BackgroundChar = bgChar;
                Redraw();
            }
            else
            {
                Console.WriteLine("Error: Invalid shape index.");
            }
        }

        public void Undo()
        {
            if (undoStack.Count > 0)
            {
                redoStack.Push(shapes.Select(s => s.Clone()).ToList());
                shapes = undoStack.Pop();
                Redraw();
            }
            else
            {
                Console.WriteLine("No actions to undo.");
            }
        }

        public void Redo()
        {
            if (redoStack.Count > 0)
            {
                undoStack.Push(shapes.Select(s => s.Clone()).ToList());
                shapes = redoStack.Pop();
                Redraw();
            }
            else
            {
                Console.WriteLine("No actions to redo.");
            }
        }

        private void SaveStateForUndo()
        {
            undoStack.Push(shapes.Select(s => s.Clone()).ToList());
            redoStack.Clear();
        }

        private bool IsWithinBounds(Shape shape)
        {
            return shape.X >= 0 && shape.Y >= 0 &&
                   shape.X + shape.Size <= Width && shape.Y + shape.Size <= Height;
        }

        public void Redraw()
        {
            Console.Clear();
            for (int i = 0; i < Height; i++)
            {
                for (int j = 0; j < Width; j++)
                {
                    Console.SetCursorPosition(j, i);
                    if (i == 0 || i == Height - 1) Console.Write("-");
                    else if (j == 0 || j == Width - 1) Console.Write("|");
                    else Console.Write(" ");
                }
            }
            foreach (var shape in shapes)
            {
                shape.Draw(Width, Height);
            }
            Console.SetCursorPosition(0, Height);
        }

        public void DisplayShapes()
        {
            if (shapes.Count == 0)
            {
                Console.WriteLine("No shapes on the canvas.");
            }
            else
            {
                for (int i = 0; i < shapes.Count; i++)
                {
                    Console.WriteLine($"{i}: {shapes[i].GetType().Name} at ({shapes[i].X}, {shapes[i].Y}), Size: {shapes[i].Size}");
                }
            }
        }

        public List<Shape> GetShapes()
        {
            return new List<Shape>(shapes);
        }
    }
}