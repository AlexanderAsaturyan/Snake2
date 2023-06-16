using System;
using System.Threading;


namespace Snake2
{
    class Snake
    {
        public int Height { get; set; } = 20;
        public int Width { get; set; } = 30;

        public int[] X = new int[600];
        public int[] Y = new int[600];

        public int FruitX { get; set; }
        public int FruitY { get; set; }

        public int PoisonX { get; set; }
        public int PoisonY { get; set; }

        public int Parts { get; set; } = 2;
        public int Score { get; set; }
        public bool GameOver { get; set; }

        ConsoleKeyInfo keyInfo = new ConsoleKeyInfo();
        ConsoleKey consoleKey = new ConsoleKey();
        ConsoleKey consoleKey2 = new ConsoleKey();

        Random rnd = new Random();

        public Snake()
        {
            X[0] = Width / 2 + 1;
            Y[0] = Height / 2 + 1;
            Console.CursorVisible = false;
            FruitX = rnd.Next(2, Width);
            FruitY = rnd.Next(2, Height);
            PoisonX = rnd.Next(2, Width);
            PoisonY = rnd.Next(2, Height);
        }

        public void Menu()
        {
            Console.SetCursorPosition(45, 10);
            Console.WriteLine("Press ENTER to start");
            while (Console.ReadKey(true).Key != ConsoleKey.Enter) ;
            Console.Clear();
        }

        public void DrawBorders()
        {
            for (int i = 1; i <= (Width + 2); i++)
            {
                Console.SetCursorPosition(i, 1);
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("*");
                Console.ResetColor();
            }
            for (int i = 1; i <= (Width + 2); i++)
            {
                Console.SetCursorPosition(i, (Height + 2));
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("*");
                Console.ResetColor();
            }

            for (int i = 1; i <= Height + 1; i++)
            {
                Console.SetCursorPosition(1, i);
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("*");
                Console.ResetColor();
            }

            for (int i = 1; i <= Height + 1; i++)
            {
                Console.SetCursorPosition(Width + 2, i);
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("*");
                Console.ResetColor();
            }

        }


        public void WritePoint(int x, int y)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.SetCursorPosition(x, y);
            Console.Write("▀");
            Console.ForegroundColor = ConsoleColor.Gray;
        }

        public void FruitWritePoint(int x, int y)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.SetCursorPosition(x, y);
            Console.Write("°");
            Console.ForegroundColor = ConsoleColor.Gray;
        }

        public void PoisonWritePoint(int x, int y)
        {
            Console.SetCursorPosition(x, y);
            Console.Write("♥");
        }

        public void Logic()
        {
            consoleKey = ConsoleKey.UpArrow;

            int thread = 200;



            while (GameOver != true)
            {
                Thread.Sleep(thread);


                Console.SetCursorPosition(45, 5);
                Console.WriteLine("Score: " + Score);


                if (X[0] == FruitX)
                {
                    if (Y[0] == FruitY)
                    {

                        Parts++;
                        Score = Score + 10;
                        if (Score > 50)
                        {
                            thread = 100;
                        }

                        FruitX = rnd.Next(2, Width - 2);
                        FruitY = rnd.Next(2, Height - 2);
                    }
                }

                if (X[0] == PoisonX)
                {
                    if (Y[0] == PoisonY)
                    {
                        if (Score == 0)
                        {
                            GameOver = true;
                            Console.Clear();
                            Console.SetCursorPosition(46, 12);
                            Console.WriteLine("You ate something wrong ☺");
                            Thread.Sleep(1000);
                        }

                        Parts--;
                        Score -= 10;
                        if (Score <= 50)
                        {
                            Thread.Sleep(200);
                        }


                        PoisonX = rnd.Next(2, Width - 2);
                        PoisonY = rnd.Next(2, Height - 2);
                        for (int i = 0; i <= (Parts - 1); i++)
                        {
                            Console.SetCursorPosition(X[Parts], Y[Parts]);
                            Console.Write(" ");
                        }
                    }

                }

                for (int i = 0; i <= (Parts - 1); i++)
                {
                    WritePoint(X[i], Y[i]);
                    Console.SetCursorPosition(X[Parts - 1], Y[Parts - 1]);
                    Console.Write(" ");
                }

                for (int i = Parts; i > 1; i--)
                {
                    X[i - 1] = X[i - 2];
                    Y[i - 1] = Y[i - 2];
                }
                consoleKey2 = consoleKey;

                if (Console.KeyAvailable)
                {
                    keyInfo = Console.ReadKey(true);
                    consoleKey = keyInfo.Key;
                }
                bool isOpposite1 = consoleKey2 == ConsoleKey.UpArrow && consoleKey == ConsoleKey.DownArrow;
                bool isOpposite2 = consoleKey2 == ConsoleKey.LeftArrow && consoleKey == ConsoleKey.RightArrow;
                bool isOpposite3 = consoleKey2 == ConsoleKey.RightArrow && consoleKey == ConsoleKey.LeftArrow;
                bool isOpposite4 = consoleKey2 == ConsoleKey.DownArrow && consoleKey == ConsoleKey.UpArrow;

                if (isOpposite1 || isOpposite2 || isOpposite3 || isOpposite4)
                {
                    consoleKey = consoleKey2;
                }

                switch (consoleKey)
                {
                    case ConsoleKey.UpArrow:
                        Y[0]--;
                        break;

                    case ConsoleKey.DownArrow:
                        Y[0]++;
                        break;

                    case ConsoleKey.LeftArrow:
                        X[0]--;
                        break;

                    case ConsoleKey.RightArrow:
                        X[0]++;
                        break;
                }


                if (Y[0] == 1 || Y[0] == 22 || X[0] == 1 || X[0] == 32)
                {
                    GameOver = true;
                }

                for (int i = 0; i < Parts; i++)
                {
                    if (X[0] == X[i + 1] && Y[0] == Y[i + 1])
                    {
                        GameOver = true;
                    }
                }

                FruitWritePoint(FruitX, FruitY);
                PoisonWritePoint(PoisonX, PoisonY);

            }

        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Random rnd = new Random();

            Snake snake = new Snake();
            snake.Menu();


        GotoPoint:
            snake.DrawBorders();
            snake.Logic();



            Console.Clear();
            Console.SetCursorPosition(45, 10);
            Console.WriteLine("You lost the game");

            Console.SetCursorPosition(46, 12);
            Console.WriteLine($"Your score is {snake.Score}");

            Console.SetCursorPosition(41, 15);
            Console.WriteLine("Press Q to quit the game");

            Console.SetCursorPosition(41, 17);
            Console.WriteLine("Press ENTER to start again");

            ConsoleKeyInfo cki;
            do
            {
                cki = Console.ReadKey(true);
            }

            while (cki.Key != ConsoleKey.Q && cki.Key != ConsoleKey.Enter);



            if (cki.Key == ConsoleKey.Enter)
            {
                snake.GameOver = false;
                Console.Clear();
                snake.Score = 0;
                snake.Parts = 2;
                snake.FruitX = rnd.Next(2, snake.Width - 2);
                snake.FruitY = rnd.Next(2, snake.Height - 2);
                snake.X[0] = snake.Width / 2 + 1;
                snake.Y[0] = snake.Height / 2 + 1;
                goto GotoPoint;
            }

            while (cki.Key != ConsoleKey.Q) { }
            Console.Clear();
        }
    }
}
