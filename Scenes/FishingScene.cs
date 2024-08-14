namespace Fishing.Scenes
{
    public struct FishBar
    {
        public bool[] bar;
        public int green;
        public int barCount;
    }

    public class FishingScene : Scene
    {
        public event Action OnFishCaught;

        public FishBar fishBar;

        public FishingScene() { type = SceneType.Fishing; }

        public override void Exit() { }

        public override void Enter()
        {
            // Enter player
            Game.game.player.Enter(type);

            // Set scene environment
            fishBar = new FishBar();

            fishBar.bar = new bool[22];
            fishBar.barCount = 0;
        }

        public override void Input()
        {
            game.inputKey = Console.ReadKey(true).Key;
        }

        public override void Render()
        {
            // Set true area avoid pointer location
            while (IsInGreen())
            {
                int green = Util.MakeRandomInt(0, fishBar.bar.Length);
                fishBar.green = green;
                for (int i = 0; i < fishBar.bar.Length; i++)
                    fishBar.bar[i] = false;
                fishBar.bar[green] = true;
            }

            PrintBar();
            PrintBarPointer();
        }

        public override void Update()
        {
            game.player.pController.Move();
            IsInGreen(true);
            CheckFishingEnd();
        }

        private bool IsInGreen(bool cntMode = false)
        {
            int barX = game.player.Position.X;
            int greenX = 2 * fishBar.green + 2;

            if (barX < greenX || barX > greenX + 3)
                return false;

            if (cntMode) fishBar.barCount++;
            return true;
        }

        private void PrintBar()
        {
            // Print boundary
            Console.BackgroundColor = ConsoleColor.DarkGray;
            // Left & Right boundary
            for (int y = 19 + 7; y < 19 + 10; y++)
            {
                Console.SetCursorPosition(0, y);
                Console.Write("  ");
                Console.SetCursorPosition(48, y);
                Console.Write("  ");
            }

            // Top & Bottom boundary
            for (int x = 0; x < 50; x += 2)
            {
                Console.SetCursorPosition(x, 19 + 7);
                Console.Write("  ");
                Console.SetCursorPosition(x, 19 + 10);
                Console.Write("  ");
            }
            Console.ResetColor();

            // Print false(black) area
            Console.BackgroundColor = ConsoleColor.Black;
            for (int x = 2; x < 47; x++)
            {
                Console.SetCursorPosition(x, 19 + 8);
                Console.Write("  ");
                Console.SetCursorPosition(x, 19 + 9);
                Console.Write("  ");
            }
            Console.ResetColor();

            // Print true(green) area
            int green = 0;
            for (int i = 0; i < fishBar.bar.Length; i++)
                if (fishBar.bar[i]) green = i;

            // Green area is near 4 tile
            green = 2 * green + 2;
            Console.BackgroundColor = ConsoleColor.DarkGreen;
            Console.SetCursorPosition(green, 19 + 8);
            Console.Write("  ");
            Console.SetCursorPosition(green, 19 + 9);
            Console.Write("  ");
            Console.SetCursorPosition(green + 2, 19 + 8);
            Console.Write("  ");
            Console.SetCursorPosition(green + 2, 19 + 9);
            Console.Write("  ");
            Console.ResetColor();
        }

        private void PrintBarPointer()
        {
            Console.SetCursorPosition(game.player.Position.X, game.player.Position.Y);
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.BackgroundColor = ConsoleColor.DarkGray;
            Console.Write(game.player.Rprsn);
            Console.ResetColor();
        }

        private void CheckFishingEnd()
        {
            if (fishBar.barCount == game.fish.Patience)
            {
                OnFishCaught?.Invoke();
                game.ChangeScene(SceneType.Fishery);
            }
        }
    }
}
