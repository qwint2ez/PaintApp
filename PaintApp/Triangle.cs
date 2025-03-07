using System;

namespace ConsolePaintApp
{
    public class Triangle : Shape
    {
        public Triangle(int x, int y, int size) : base(x, y, size) { }

        public override void Draw(int canvasWidth, int canvasHeight)
        {
            int height = Size;
            for (int i = 0; i < height; i++)
            {
                int startX = X - i;
                int endX = X + i;
                for (int j = startX; j <= endX; j++)
                {
                    int drawX = j;
                    int drawY = Y + i;
                    if (drawX >= 0 && drawX < canvasWidth && drawY >= 0 && drawY < canvasHeight)
                    {
                        Console.SetCursorPosition(drawX, drawY);
                        if (i == height - 1 || j == startX || j == endX)
                            Console.Write(FillChar);
                        else
                            Console.Write(BackgroundChar);
                    }
                }
            }
        }

        public override Shape Clone()
        {
            return new Triangle(X, Y, Size) { FillChar = FillChar, BackgroundChar = BackgroundChar };
        }
    }
}