using System;

namespace ConsolePaintApp
{
    class Square : Shape
    {
        public Square(int x, int y, int size) : base(x, y, size) { }

        public override void Draw(int canvasWidth, int canvasHeight)
        {
            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    int drawX = X + j;
                    int drawY = Y + i;
                    if (drawX >= 0 && drawX < canvasWidth && drawY >= 0 && drawY < canvasHeight)
                    {
                        Console.SetCursorPosition(drawX, drawY);
                        if (i == 0 || i == Size - 1 || j == 0 || j == Size - 1)
                            Console.Write(FillChar);
                        else
                            Console.Write(BackgroundChar);
                    }
                }
            }
        }
    }
}