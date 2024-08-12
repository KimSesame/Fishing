using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Fishing.Scenes
{
    public struct Point
    {
        public int x;
        public int y;
    }

    public class FisheryScene : Scene
    {
        public bool fishExist;

        public FisheryTile[,] fishery;
        public Point fishPos;
        public Point playerPos;

        const int FISHERY_WIDTH = 17;
        const int FISHERY_HEIGHT = 15;

        public override void Exit() { }

        public override void Enter()
        {

            fishExist = false;
            fishery = new FisheryTile[FISHERY_HEIGHT, FISHERY_WIDTH];
            playerPos = new Point() { x = 24, y = 19 };
            
        }

        public override void Input()
        {
            game.inputKey = Console.ReadKey(true).Key;
        }

        public override void Render()
        {
            Console.Clear();

            // Refresh fishery if fish is NOT exist
            if (!fishExist)
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

        public override void Update()
        {
            Move();
        }

        private void RefreshFishery()
        {
            for (int i = 0; i < FISHERY_HEIGHT; i++)
                for (int j = 0; j < FISHERY_WIDTH; j++)
                {
                    fishery[i, j] = FisheryTile.None;
                }
        }

        private void GenerateFish()
        {
            int x = Util.MakeRandomInt(1, FISHERY_WIDTH - 1);
            int y = Util.MakeRandomInt(1, FISHERY_HEIGHT - 2);

            fishPos = new Point() { x = 2 * x + 8, y = y + 3 };
            fishExist = true;
        }

        private void GenerateWave(int n)
        {
            int x, y;
            for (int i = 0; i < n; i++)
            {
                x = Util.MakeRandomInt(0, FISHERY_HEIGHT);
                y = Util.MakeRandomInt(0, FISHERY_WIDTH);

                fishery[x, y] = FisheryTile.Wave;
            }
        }

        private void GenerateWall()
        {
            // Left & Right boundary
            for (int i = 0; i < FISHERY_HEIGHT; i++)
            {
                fishery[i, 0] = FisheryTile.Wall;
                fishery[i, FISHERY_WIDTH - 1] = FisheryTile.Wall;
            }

            // Top & Bottom boundary
            for (int j = 0; j < FISHERY_WIDTH; j++)
            {
                fishery[0, j] = FisheryTile.Wall;
                fishery[FISHERY_HEIGHT - 1, j] = FisheryTile.Wall;
            }
        }

        private void PrintFishery()
        {
            Console.Write("\n\n\n");
            for (int i = 0; i < FISHERY_HEIGHT; i++)
            {
                Console.Write("        ");
                for (int j = 0; j < FISHERY_WIDTH; j++)
                {
                    switch (fishery[i, j])
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

        private void PrintFish(bool fishing = false)
        {
            Console.SetCursorPosition(fishPos.x, fishPos.y);
            Console.ForegroundColor = (fishing) ? ConsoleColor.Yellow : ConsoleColor.DarkRed;
            Console.BackgroundColor = ConsoleColor.Blue;
            Console.Write("★");
            Console.ResetColor();
        }

        private void PrintPlayer()
        {
            Console.SetCursorPosition(playerPos.x, playerPos.y);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("▲");
            Console.ResetColor();
        }

        private void PrintScore()
        {
            Console.SetCursorPosition(0, 25);
            Console.Write($"Score: {game.score}");
        }

        private void Move()
        {
            switch (game.inputKey)
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
                    game.Over();
                    break;
                default:
                    break;
            }
        }

        private void MoveLeft()
        {
            Point next = new Point() { x = playerPos.x - 2, y = playerPos.y };
         
            if (next.x > 8)
                playerPos = next;
        }

        private void MoveRight()
        {
            Point next = new Point() { x = playerPos.x + 2, y = playerPos.y };
         
            if (next.x < 40)
                playerPos = next;
        }

        private bool IsFishColumn()
        {
            return playerPos.x == fishPos.x;
        }

        private void ThrowRod()
        {
            int x = playerPos.x;
            int y = playerPos.y - 1;

            Console.ForegroundColor = ConsoleColor.DarkRed;

            // Nothing happen
            if (!IsFishColumn())
            {
                // Print effect(?)
                Console.SetCursorPosition(x + 2, playerPos.y);
                Console.Write("?");
                Console.Beep(500, 500);
                Console.ResetColor();
                return;
            }

            // Fish bite!
            // Print effect(!)
            Console.SetCursorPosition(x + 2, playerPos.y);
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
            for (; y > fishPos.y + 1; y--)
            {
                Console.SetCursorPosition(x, y);
                Console.Write("∥");
            }

            // End of fishing rod
            Console.SetCursorPosition(x, y);
            Console.Write("§");
            Console.ResetColor();

            // Call FishingScene
            PrintFish(true);
            Console.Beep(1000, 500);
            game.ChangeScene(SceneType.Fishing);
        }

    }
}
