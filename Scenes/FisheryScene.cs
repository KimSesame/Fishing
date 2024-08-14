using Fishing.Controllers;

namespace Fishing.Scenes
{
    public class FisheryScene : Scene
    {
        const int FISHERY_WIDTH = 17;
        const int FISHERY_HEIGHT = 15;

        private FisheryTile[,] fishery;

        public FisheryScene() { type = SceneType.Fishery; }

        public override void Exit() { }

        public override void Enter()
        {
            // Enter player
            game.player.Enter(type);

            // Set scene environment
            fishery = new FisheryTile[FISHERY_HEIGHT, FISHERY_WIDTH];

            RefreshFishery();
            GenerateWave(20);
            GenerateWall();
            GenerateFish();
        }

        public override void Input()
        {
            game.inputKey = Console.ReadKey(true).Key;
        }

        public override void Render()
        {
            Console.Clear();

            PrintFishery();
            PrintFish();
            PrintPlayer();
            game.PrintScore();
        }

        public override void Update()
        {
            game.player.pController.Move();
        }

        public void CatchFish()
        {
            game.Score += game.fish.Patience;
            game.FishCount++;
            game.fish = null;
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

            game.fish = new Fish(2 * x + 8, y + 3);
            (game.player.pController as PlayerController).OnRodThrowed += game.fish.Bite;
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
            Console.SetCursorPosition(game.fish.Position.X, game.fish.Position.Y);
            Console.ForegroundColor = (fishing) ? ConsoleColor.Yellow : ConsoleColor.DarkRed;
            Console.BackgroundColor = ConsoleColor.Blue;
            Console.Write(game.fish.Rprsn);
            Console.ResetColor();
        }

        private void PrintPlayer()
        {
            Console.SetCursorPosition(game.player.Position.X, game.player.Position.Y);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write(game.player.Rprsn);
            Console.ResetColor();
        }
    }
}
