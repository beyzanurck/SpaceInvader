using System;
using System.Text;
using System.Threading;

namespace spaceInvader
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.Unicode;

            drawFrame();

            //player info
            int[] x = new int[5];
            x[0] = 82;

            int[] y = new int[5];
            y[0] = 32;

            int[] yFireMoving = new int[3];
            int[] xFireMoving = new int[3];
            int xFirePos, yFirePos = 0;
            bool isFireExist = false;

            // enemy info
            int[] xFirstLineEnemy = new int[6];
            xFirstLineEnemy[0] = 60; xFirstLineEnemy[1] = 66; xFirstLineEnemy[2] = 72;
            xFirstLineEnemy[3] = 78; xFirstLineEnemy[4] = 84; xFirstLineEnemy[5] = 90;

            int[] yFirstLineEnemy = new int[7];
            yFirstLineEnemy[0] = 19; yFirstLineEnemy[1] = 19; yFirstLineEnemy[2] = 19;
            yFirstLineEnemy[3] = 19; yFirstLineEnemy[4] = 19; yFirstLineEnemy[5] = 19;

            int[] xSecondLineEnemy = new int[6];
            xSecondLineEnemy[0] = 60; xSecondLineEnemy[1] = 66; xSecondLineEnemy[2] = 72;
            xSecondLineEnemy[3] = 78; xSecondLineEnemy[4] = 84; xSecondLineEnemy[5] = 90;

            int[] ySecondLineEnemy = new int[7];
            ySecondLineEnemy[0] = 18; ySecondLineEnemy[1] = 18; ySecondLineEnemy[2] = 18;
            ySecondLineEnemy[3] = 18; ySecondLineEnemy[4] = 18; ySecondLineEnemy[5] = 18;

            int directionOfEnemy = 1;
            int counterForHands = 0;
            int speedOfEnemy = 0;

            int[] yFireMovingOfEnemy = new int[3];
            int[] xFireMovingOfEnemy = new int[3];
            int timeOfEnemyFire = 0;
            int xFirePosOfEnemy, yFirePosOfEnemy = 0;
            bool isFireExistOfEnemy = false;

            Random random = new Random();

            bool[] destroyEnemy = new bool[6];
            destroyEnemy[0] = false; destroyEnemy[1] = false; destroyEnemy[2] = false;
            destroyEnemy[3] = false; destroyEnemy[4] = false; destroyEnemy[5] = false;
            int[] falseEnemy = new int[6];

            bool[] destroyEnemyOfSecond = new bool[6];
            destroyEnemyOfSecond[0] = false; destroyEnemyOfSecond[1] = false; destroyEnemyOfSecond[2] = false;
            destroyEnemyOfSecond[3] = false; destroyEnemyOfSecond[4] = false; destroyEnemyOfSecond[5] = false;

            int score = 0;
            int counterForWinner = 0;

            while (true)
            {
                Console.SetCursorPosition(110, 4);
                Console.Write("Your Score: {0}", score);

                timeOfEnemyFire++;

                speedOfEnemy++;

                if (speedOfEnemy % 3 == 0)
                {
                    moveEnemyFirstLine(ref xFirstLineEnemy, ref yFirstLineEnemy, ref directionOfEnemy);

                    drawEnemyFirstLine(ref xFirstLineEnemy, ref yFirstLineEnemy, ref counterForHands, ref destroyEnemy);
                }

                if (Console.KeyAvailable)
                {
                    ConsoleKeyInfo keyInfo = Console.ReadKey();
                    while (Console.KeyAvailable)
                    {
                        keyInfo = Console.ReadKey();
                    }

                    drawPlayer(ref x, ref y, false);

                    switch (keyInfo.Key)
                    {
                        case ConsoleKey.RightArrow:

                            if (x[0] != 127)
                            {
                                x[0 + 1] = x[0] + 5;
                                x[0] = x[0 + 1];

                                y[0 + 1] = y[0];
                                y[0] = y[0 + 1];
                            }

                            break;

                        case ConsoleKey.LeftArrow:

                            if (x[0] != 37)
                            {
                                x[0 + 1] = x[0] - 5;
                                x[0] = x[0 + 1];

                                y[0 + 1] = y[0];
                                y[0] = y[0 + 1];
                            }

                            break;

                        case ConsoleKey.Spacebar:

                            if (isFireExist == false)
                            {
                                xFireMoving[0] = x[0];
                                yFireMoving[0] = y[0] - 1;
                                isFireExist = true;
                                fireOfPlayer(xFireMoving[0], yFireMoving[0]);
                            }

                            break;
                    }
                }

                drawPlayer(ref x, ref y, true);

                if (yFireMoving[0] != 16 && isFireExist == true)
                {
                    yFirePos = yFireMoving[0];
                    xFirePos = xFireMoving[0];

                    yFireMoving[1] = yFireMoving[0] - 1;
                    yFireMoving[0] = yFireMoving[1];

                    xFireMoving[1] = xFireMoving[0];
                    xFireMoving[0] = xFireMoving[1];

                    Console.SetCursorPosition(xFirePos, yFirePos);
                    Console.Write(" ");

                    fireOfPlayer(xFireMoving[0], yFireMoving[0]);
                }

                if (yFireMoving[0] == 16)
                {
                    isFireExist = false;

                    Console.SetCursorPosition(xFireMoving[0], yFireMoving[0]);
                    Console.Write(" ");
                }

                // the management of enemy fire
                int n = random.Next(0, 6);
                if (timeOfEnemyFire % 7 == 0 && isFireExistOfEnemy == false)
                {
                    for (int i = 0; i < destroyEnemy.Length; i++)
                    {
                        if (destroyEnemy[i])
                        {
                            n = -1;
                            falseEnemy[i] = -1;
                        }
                        else
                        {
                            falseEnemy[i] = i;
                        }
                    }

                    while (n == -1)
                    {
                        n = falseEnemy[random.Next(0, 6)];
                    }

                    isFireExistOfEnemy = true;
                    xFireMovingOfEnemy[0] = xFirstLineEnemy[n] + 2;
                    yFireMovingOfEnemy[0] = yFirstLineEnemy[n] + 1;
                    fireOfEnemy(xFireMovingOfEnemy[0], yFireMovingOfEnemy[0]);
                }

                // moving of enemy fire
                if (isFireExistOfEnemy == true)
                {
                    xFirePosOfEnemy = xFireMovingOfEnemy[0];
                    yFirePosOfEnemy = yFireMovingOfEnemy[0];

                    yFireMovingOfEnemy[1] = yFireMovingOfEnemy[0] + 1;
                    yFireMovingOfEnemy[0] = yFireMovingOfEnemy[1];

                    xFireMovingOfEnemy[1] = xFireMovingOfEnemy[0];
                    xFireMovingOfEnemy[0] = xFireMovingOfEnemy[1];

                    Console.SetCursorPosition(xFirePosOfEnemy, yFirePosOfEnemy);
                    Console.Write(" ");

                    fireOfEnemy(xFireMovingOfEnemy[0], yFireMovingOfEnemy[0]);
                }

                // border of enemy fire
                if (yFireMovingOfEnemy[0] == 34)
                {
                    isFireExistOfEnemy = false;

                    Console.SetCursorPosition(xFireMovingOfEnemy[0], yFireMovingOfEnemy[0]);
                    Console.Write(" ");
                }

                // destroy an enemy due to player fire
                for (int i = 0; i < xFirstLineEnemy.Length; i++)
                {
                    if (destroyEnemy[i] != true)
                    {
                        if (xFirstLineEnemy[i] <= xFireMoving[0] && xFireMoving[0] <= xFirstLineEnemy[i] + 4 && yFirstLineEnemy[i] == yFireMoving[0])
                        {
                            Console.SetCursorPosition(xFirstLineEnemy[i], yFirstLineEnemy[i]);
                            Console.Write("     ");

                            destroyEnemy[i] = true;
                            counterForWinner++;
                            score = 10 * counterForWinner;

                            yFireMoving[0] = 16;
                            break;
                        }
                    }
                }

                // player death due to enemy fire
                if (xFireMovingOfEnemy[0] == x[0] && yFireMovingOfEnemy[0] == y[0] || x[0] - 1 <= xFireMovingOfEnemy[0] && xFireMovingOfEnemy[0] <= x[0] + 1 && y[0] + 1 == yFireMovingOfEnemy[0])
                {
                    drawPlayer(ref x, ref y, false);
                    Thread.Sleep(200);
                    break;
                }

                //check if the enemy line and the player are in the same coordinates
                if (y[0] == yFirstLineEnemy[0])
                {
                    // this place is death animation
                    Thread.Sleep(200);
                    break;
                }


                if (counterForWinner == 6)
                {
                    break;
                }
                Thread.Sleep(90);
            }

            drawFrame();

            seeYourLastScore(counterForWinner, score);

            Console.ReadLine();

        }

        public static void drawFrame()
        {
            Console.CursorVisible = false;

            for (int y = 5; y <= 35; y++)
            {
                for (int x = 35; x <= 130; x++)
                {
                    Console.SetCursorPosition(x, y);

                    if (x == 35 || x == 130)
                    {
                        Console.Write("|");
                    }
                    else if (y == 5 || y == 35)
                    {
                        Console.Write("-");
                    }
                    else
                    {
                        Console.Write(" ");
                    }
                }
                Console.WriteLine();
            }
        }
        public static void drawPlayer(ref int[] x, ref int[] y, bool isExist)
        {
            if (isExist == true)
            {
                Console.SetCursorPosition(x[0], y[0]);
                Console.Write("^");
                Console.SetCursorPosition(x[0] - 1, y[0] + 1);
                Console.Write("<O>");
            }
            else
            {
                Console.SetCursorPosition(x[0], y[0]);
                Console.Write(" ");

                Console.SetCursorPosition(x[0] - 1, y[0] + 1);
                Console.Write("   "); // 3 space for moving 5 unit of player
            }
        }

        public static void fireOfPlayer(int x, int y)
        {
            Console.SetCursorPosition(x, y);
            Console.Write("╿");
        }

        public static void fireOfEnemy(int x, int y)
        {
            Console.SetCursorPosition(x, y);
            Console.Write("╽");
        }

        public static void drawEnemyFirstLine(ref int[] xFirstLineEnemy, ref int[] yFirstLineEnemy, ref int counter, ref bool[] destroyEnemy)
        {
            counter++;

            if (counter % 2 != 0)
            {
                for (int i = 0; i < xFirstLineEnemy.Length; i++)
                {
                    Console.SetCursorPosition(xFirstLineEnemy[i], yFirstLineEnemy[i]);

                    if (destroyEnemy[i])
                        Console.Write("     ");
                    else
                    {
                        Console.Write("╲○▴○╱");
                    }
                }
            }
            else
            {
                for (int i = 0; i < xFirstLineEnemy.Length; i++)
                {
                    Console.SetCursorPosition(xFirstLineEnemy[i], yFirstLineEnemy[i]);

                    if (destroyEnemy[i])
                        Console.Write("     ");
                    else
                    {
                        Console.Write("╱○▴○╲");
                    }
                }
            }
        }
        public static void moveEnemyFirstLine(ref int[] xFirstLineEnemy, ref int[] yFirstLineEnemy, ref int direction)
        {
            if (xFirstLineEnemy[5] == 114)
            {
                direction = -1;

                deleteTheEnemyLine(ref xFirstLineEnemy, ref yFirstLineEnemy);
                for (int i = 0; i < 6; i++)
                {
                    yFirstLineEnemy[i] = yFirstLineEnemy[i] + 1;
                }
            }

            if (xFirstLineEnemy[0] == 42)
            {
                direction = 1;

                deleteTheEnemyLine(ref xFirstLineEnemy, ref yFirstLineEnemy);
                for (int i = 0; i < 6; i++)
                {
                    yFirstLineEnemy[i] = yFirstLineEnemy[i] + 1;
                }
            }

            if (direction == 1)
            {
                Console.SetCursorPosition(xFirstLineEnemy[0], yFirstLineEnemy[0]);
                Console.Write("     ");

                for (int i = 0; i < xFirstLineEnemy.Length; i++)
                {
                    xFirstLineEnemy[i] = xFirstLineEnemy[i] + 6;
                }

            }
            else
            {
                Console.SetCursorPosition(xFirstLineEnemy[5], yFirstLineEnemy[5]);
                Console.Write("     ");

                for (int i = 0; i < xFirstLineEnemy.Length; i++)
                {
                    xFirstLineEnemy[i] = xFirstLineEnemy[i] - 6;
                }

            }
        }
        public static void deleteTheEnemyLine(ref int[] x, ref int[] y)
        {
            for (int i = 0; i < x.Length; i++)
            {
                Console.SetCursorPosition(x[i], y[i]);
                Console.Write("     ");
            }
        }
        public static void seeYourLastScore(int counter, int score)
        {
            Console.SetCursorPosition(110, 4);
            Console.Write("               ");

            Console.SetCursorPosition(65, 20);
            if (counter != 6)
            {
                Console.Write("GAME OVER");
            }
            else
            {
                Console.Write("YOU WIN!!");
            }
            Console.SetCursorPosition(65, 22);
            Console.Write("Your Score: {0}", score);
        }
    }
}





