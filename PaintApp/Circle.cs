using System;

namespace ConsolePaintApp
{
    class Circle : Shape
    {
        public Circle(int x, int y, int size) : base(x, y, size) { }

        public override void Draw(int canvasWidth, int canvasHeight)
        {
            for (int i = -Size; i <= Size; i++)
            {
                for (int j = -Size; j <= Size; j++)
                {
                    double distance = Math.Sqrt(i * i + j * j);
                    int drawX = X + j;
                    int drawY = Y + i;
                    if (drawX >= 0 && drawX < canvasWidth && drawY >= 0 && drawY < canvasHeight)
                    {
                        if (Math.Abs(distance - Size) < 0.5)
                        {
                            Console.SetCursorPosition(drawX, drawY);
                            Console.Write(FillChar); // Outline
                        }
                        else if (distance < Size)
                        {
                            Console.SetCursorPosition(drawX, drawY);
                            Console.Write(BackgroundChar); // Background
                        }
                    }
                }
            }
        }
    }
}