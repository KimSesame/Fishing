using System.Data;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Fishing
{
    internal class Program
    {
        public enum FisheryTile
        {
            None, Wave, Wall
        }

        public struct GameData
        {
            public bool running;
            public bool fishing;

            public bool fishExist;

            public FishBar fishBar;
            public FisheryTile[,] fishery;
            public ConsoleKey inputKey;
            public Point fishPos;
            public Point playerPos;
            public int score;
        }

        public struct FishBar
        {
            public bool[] bar;
            public Point barPointerPos;
            public int green;
            public int barCount;
            public int target;
        }
        public struct Point
        {
            public int x;
            public int y;
        }

        const int FISHERY_WIDTH = 17;
        const int FISHERY_HEIGHT = 15;

        static GameData data;

        static void Main(string[] args)
        {
            Start();

            while (data.running)
            {
                // Move to fishing
                while (data.running && !data.fishing)
                {
                    RenderFishery();
                    Input();
                    UpdatePlayer();
                }

                // Fishing
                while(data.fishing)
                {
                    RenderFishBar();
                    Input();
                    UpdateBar();
                }
            }
        }

        static int MakeRandomInt(int inclusiveMin, int exclusiveMax)
        {
            Random rand = new();
            return rand.Next(inclusiveMin, exclusiveMax);
        }

        static void Start()
        {
            // Console configuration
            Console.Title = "FISHing";
            Console.CursorVisible = false;
            Console.SetWindowSize(50, 30);

            // Initialize GameData
            data = new();
            data.fishBar = new();

            data.running = true;
            data.fishing = false;
            data.fishExist = false;
            data.fishery = new FisheryTile[FISHERY_HEIGHT, FISHERY_WIDTH];
            data.playerPos = new Point() { x = 24, y = 19 };
            data.score = 0;
            data.fishBar.bar = new bool[22];
            data.fishBar.barPointerPos = new Point() { x = 24, y = 26 };

            PrintMainMenu();
            Console.ReadKey();
        }

        static void PrintMainMenu()
        {
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("     ■■■ ■ ■■■ ■  ■                          ");
            Console.WriteLine("     ■     ■ ■     ■  ■ ■                    ");
            Console.WriteLine("     ■■■ ■ ■■■ ■■■ ■ ■■■ ■■■                 ");
            Console.WriteLine("     ■     ■     ■ ■  ■ ■ ■  ■ ■  ■          ");
            Console.WriteLine("     ■     ■ ■■■ ■  ■ ■ ■  ■ ■■■             ");
            Console.WriteLine("                                           ■");
            Console.WriteLine("                                     ┌ ■  ■");
            Console.WriteLine("                                     │ ■■■");
            Console.WriteLine("                                     │");
            Console.WriteLine("                                     │");
            Console.WriteLine("     Move LEFT or RIGHT              │");
            Console.WriteLine("     Spacebar to start fishing!      │");
            Console.WriteLine("     ESC to quit.                    │");
            Console.WriteLine("                                     │");
            Console.WriteLine("                  ~Press Any Key~    │");
            Console.WriteLine("                                     │");
            Console.WriteLine("        ■                           │");
            Console.WriteLine("        ■■  ■■■                 │");
            Console.WriteLine("        ■■■■■▣■               │");
            Console.WriteLine("        ■■■■■■■               ¿");
            Console.WriteLine("        ■■  ■■■                ");
            Console.WriteLine("        ■                      ");
            Console.WriteLine("                               ");
        }

        static void RenderFishery()
        {
            Console.Clear();

            // Refresh fishery if fish is NOT exist
            if(!data.fishExist)
            {
                RefreshFishery();
                GenerateWave(20);
                GenerateWall();
                GenerateFish();
            }

            PrintFishery();
            PrintFish();
            PrintPlayer();
            PrintScore();
        }

        static void RefreshFishery()
        {
            for (int i = 0; i < FISHERY_HEIGHT; i++)
                for (int j = 0; j < FISHERY_WIDTH; j++)
                {
                    data.fishery[i, j] = FisheryTile.None;
                }
        }

        static void GenerateFish()
        {
            int x = MakeRandomInt(1, FISHERY_WIDTH - 1);
            int y = MakeRandomInt(1, FISHERY_HEIGHT - 2);

            data.fishPos = new Point() { x = 2 * x + 8, y = y + 3 };
            data.fishExist = true;
        }

        static void GenerateWave(int n)
        {
            int x, y;
            for(int i = 0; i < n; i++)
            {
                x = MakeRandomInt(0, FISHERY_HEIGHT);
                y = MakeRandomInt(0, FISHERY_WIDTH);

                data.fishery[x, y] = FisheryTile.Wave;
            }
        }

        static void GenerateWall()
        {
            // Left & Right boundary
            for (int i=0; i<FISHERY_HEIGHT; i++)
            {
                data.fishery[i, 0] = FisheryTile.Wall;
                data.fishery[i, FISHERY_WIDTH-1] = FisheryTile.Wall;
            }

            // Top & Bottom boundary
            for (int j = 0; j < FISHERY_WIDTH; j++)
            {
                data.fishery[0, j] = FisheryTile.Wall;
                data.fishery[FISHERY_HEIGHT - 1, j] = FisheryTile.Wall;
            }
        }

        static void PrintFishery()
        {
            Console.Write("\n\n\n");
            for (int i = 0; i < FISHERY_HEIGHT; i++)
            {
                Console.Write("        ");
                for (int j = 0; j < FISHERY_WIDTH; j++)
                {
                    switch (data.fishery[i, j])
                    {
                        case FisheryTile.Wave:
                            Console.BackgroundColor = ConsoleColor.Blue;
                            Console.Write("~ ");
                            Console.ResetColor();
                            break;
                        case FisheryTile.Wall:
                            Console.BackgroundColor = ConsoleColor.DarkBlue;
                            Console.Write("■");
                            Console.ResetColor();
                            break;
                        case FisheryTile.None:
                            Console.BackgroundColor = ConsoleColor.Blue;
                            Console.Write("  ");
                            Console.ResetColor();
                            break;
                        default:
                            break;
                    }
                }
                Console.WriteLine();
            }
        }

        static void PrintFish()
        {
            Console.SetCursorPosition(data.fishPos.x, data.fishPos.y);
            Console.ForegroundColor = (data.fishing) ? ConsoleColor.Yellow : ConsoleColor.DarkRed;
            Console.BackgroundColor = ConsoleColor.Blue;
            Console.Write("★");
            Console.ResetColor();
        }

        static void PrintPlayer()
        {
            Console.SetCursorPosition(data.playerPos.x, data.playerPos.y);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("▲");
            Console.ResetColor();
        }

        static void Input()
        {
            data.inputKey = Console.ReadKey(true).Key;
        }

        static void UpdatePlayer()
        {
            Move();
        }

        static void UpdateBar()
        {
            Move();
            CheckFishingEnd();
        }

        static void Move()
        {
            switch (data.inputKey)
            {
                case ConsoleKey.A:
                case ConsoleKey.LeftArrow:
                    MoveLeft();
                    break;
                case ConsoleKey.D:
                case ConsoleKey.RightArrow:
                    MoveRight();
                    break;
                case ConsoleKey.Spacebar:
                    ThrowRod();
                    break;
                case ConsoleKey.Escape:
                    EndGame();
                    break;
                default:
                    break;
            }
        }

        static void MoveLeft()
        {
            // Move player while NOT fishing
            if(!data.fishing)
            {
                Point next = new Point() { x = data.playerPos.x - 2, y = data.playerPos.y };
                if (next.x > 8)
                {
                    data.playerPos = next;
                }
            }
            // Move fish bar pointer while fishing
            else if (data.fishing)
            {
                Point next = new Point() { x = data.fishBar.barPointerPos.x - 2, y = data.fishBar.barPointerPos.y };
                if (next.x > 0)
                {
                    data.fishBar.barPointerPos = next;
                    IsInGreen(true);
                }
            }
        }

        static void MoveRight()
        {
            // Move player while NOT fishing
            if (!data.fishing)
            {
                Point next = new Point() { x = data.playerPos.x + 2, y = data.playerPos.y };
                if (next.x < 40)
                {
                    data.playerPos = next;
                }
            }
            // Move fish bar pointer while fishing
            else if (data.fishing)
            {
                Point next = new Point() { x = data.fishBar.barPointerPos.x + 2, y = data.fishBar.barPointerPos.y };
                if (next.x < 48)
                {
                    data.fishBar.barPointerPos = next;
                    IsInGreen(true);
                }
            }
        }

        static bool IsFishColumn()
        {
            return data.playerPos.x == data.fishPos.x;
        }

        static void ThrowRod()
        {
            if (data.fishing) return;

            int x = data.playerPos.x;
            int y = data.playerPos.y - 1;

            Console.ForegroundColor = ConsoleColor.DarkRed;

            // Nothing happen
            if(!IsFishColumn())
            {
                // Print effect(?)
                Console.SetCursorPosition(x + 2, data.playerPos.y);
                Console.Write("?");
                Console.Beep(500, 500);
                Console.ResetColor();
                return;
            }

            // Fish bite!
            // Print effect(!)
            Console.SetCursorPosition(x+2, data.playerPos.y);
            Console.Write("!");

            // Rod at ground
            Console.BackgroundColor = ConsoleColor.Black;
            Console.SetCursorPosition(x, y--);
            Console.Write("∥");
            
            // Rod at wall
            Console.BackgroundColor = ConsoleColor.Gray;
            Console.SetCursorPosition(x, y--);
            Console.Write("∥");

            // Rod at water
            Console.BackgroundColor = ConsoleColor.Blue;
            for (; y > data.fishPos.y + 1; y--) 
            {
                Console.SetCursorPosition(x, y);
                Console.Write("∥");
            }

            // End of fishing rod
            Console.SetCursorPosition(x, y);
            Console.Write("§");
            Console.ResetColor();

            data.fishing = true;
            data.fishBar.barCount = 0;
            data.fishBar.target = MakeRandomInt(3, 6);

            PrintFish();
            Console.Beep(1000, 500);
        }

        static void RenderFishBar()
        {
            // Set true area avoid pointer location
            while (IsInGreen())
            {
                int green = MakeRandomInt(0, data.fishBar.bar.Length);
                data.fishBar.green = green;
                for (int i = 0; i < data.fishBar.bar.Length; i++)
                    data.fishBar.bar[i] = false;
                data.fishBar.bar[green] = true;
            }

            PrintBar();
            PrintBarPointer();
        }

        static bool IsInGreen(bool cntMode = false)
        {
            int barX = data.fishBar.barPointerPos.x;
            int greenX = 2 * data.fishBar.green + 2;

            if (barX < greenX || barX > greenX + 3)
                return false;

            if (cntMode) data.fishBar.barCount++;
            return true;
        }

        static void PrintBar()
        {
            // Print boundary
            Console.BackgroundColor = ConsoleColor.DarkGray;
            // Left & Right boundary
            for (int y = data.playerPos.y + 7; y < data.playerPos.y + 10; y++)
            {
                Console.SetCursorPosition(0, y);
                Console.Write("  ");
                Console.SetCursorPosition(48, y);
                Console.Write("  ");
            }

            // Top & Bottom boundary
            for (int x = 0; x < 50; x += 2)
            {
                Console.SetCursorPosition(x, data.playerPos.y + 7);
                Console.Write("  ");
                Console.SetCursorPosition(x, data.playerPos.y + 10);
                Console.Write("  ");
            }
            Console.ResetColor();

            // Print false(black) area
            Console.BackgroundColor = ConsoleColor.Black;
            for (int x = 2; x < 47; x++) 
            {
                Console.SetCursorPosition(x, data.playerPos.y + 8);
                Console.Write("  ");
                Console.SetCursorPosition(x, data.playerPos.y + 9);
                Console.Write("  ");
            }
            Console.ResetColor();

            // Print true(green) area
            int green = 0;
            for (int i = 0; i < data.fishBar.bar.Length; i++)
                if (data.fishBar.bar[i]) green = i;

            // Green area is near 4 tile
            green = 2 * green + 2;
            Console.BackgroundColor = ConsoleColor.DarkGreen;
            Console.SetCursorPosition(green, data.playerPos.y + 8);
            Console.Write("  ");
            Console.SetCursorPosition(green, data.playerPos.y + 9);
            Console.Write("  ");
            Console.SetCursorPosition(green + 2, data.playerPos.y + 8);
            Console.Write("  ");
            Console.SetCursorPosition(green + 2, data.playerPos.y + 9);
            Console.Write("  ");
            Console.ResetColor();
        }

        static void PrintBarPointer()
        {
            Console.SetCursorPosition(data.fishBar.barPointerPos.x, data.fishBar.barPointerPos.y);
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.BackgroundColor = ConsoleColor.DarkGray;
            Console.Write("▼");
            Console.ResetColor();
        }

        static void CheckFishingEnd()
        {
            if (data.fishBar.barCount == data.fishBar.target)
            { 
                data.fishExist = false;
                data.fishing = false;
                data.score++;
            }
        }

        static void EndGame()
        {
            if (data.fishing) return;

            Console.Clear();
            data.running = false;
        }

        static void PrintScore()
        {
            Console.SetCursorPosition(0, data.fishBar.barPointerPos.y - 1);
            Console.Write($"Score: {data.score}");
        }
    }
}
