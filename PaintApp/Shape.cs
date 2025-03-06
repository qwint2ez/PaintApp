using System;

namespace ConsolePaintApp
{
    abstract class Shape
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Size { get; set; }
        public char FillChar { get; set; } = '*';
        public char BackgroundChar { get; set; } = ' ';

        public Shape(int x, int y, int size)
        {
            X = x;
            Y = y;
            Size = size;
        }

        public abstract void Draw(int canvasWidth, int canvasHeight);
        public void Move(int newX, int newY)
        {
            X = newX;
            Y = newY;
        }
    }
}