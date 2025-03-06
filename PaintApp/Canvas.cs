using System;
using System.Collections.Generic;

namespace ConsolePaintApp
{
    class Canvas
    {
        private List<Shape> shapes = new List<Shape>();
        private Stack<List<Shape>> undoStack = new Stack<List<Shape>>();
        private Stack<List<Shape>> redoStack = new Stack<List<Shape>>();
        public int Width { get; }
        public int Height { get; }

        public Canvas(int width, int height)
        {
            Width = width;
            Height = height;
            Redraw();
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
                Shape temp = shapes[index];
                temp.Move(newX, newY);
                if (IsWithinBounds(temp))
                {
                    SaveStateForUndo();
                    shapes[index] = temp;
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
                redoStack.Push(new List<Shape>(shapes));
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
                undoStack.Push(new List<Shape>(shapes));
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
            undoStack.Push(new List<Shape>(shapes));
            redoStack.Clear(); // Очищаем стек redo при новом действии
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
            return new List<Shape>(shapes); // Возвращаем копию для безопасности
        }
    }
}