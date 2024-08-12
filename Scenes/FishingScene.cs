using System.Reflection;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Fishing.Scenes
{
    public struct FishBar
    {
        public bool[] bar;
        public Point barPointerPos;
        public int green;
        public int barCount;
        public int target;
    }

    public class FishingScene : Scene
    {
        public FishBar fishBar;

        public override void Exit() { }

        public override void Enter()
        {
            fishBar = new FishBar();

            fishBar.bar = new bool[22];
            fishBar.barPointerPos = new Point() { x = 24, y = 26 };
            fishBar.barCount = 0;
            fishBar.target = Util.MakeRandomInt(3, 6);
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
            Move();
            CheckFishingEnd();
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
                case ConsoleKey.Escape:
                    game.ChangeScene(SceneType.Fishery);
                    break;
                default:
                    break;
            }
        }

        private void MoveLeft()
        {
            Point next = new Point() { x = fishBar.barPointerPos.x - 2, y = fishBar.barPointerPos.y };
            if (next.x > 0)
            {
                fishBar.barPointerPos = next;
                IsInGreen(true);
            }
        }

        private void MoveRight()
        {
            Point next = new Point() { x = fishBar.barPointerPos.x + 2, y = fishBar.barPointerPos.y };
            if (next.x < 48)
            {
                fishBar.barPointerPos = next;
                IsInGreen(true);
            }
        }

        private bool IsInGreen(bool cntMode = false)
        {
            int barX = fishBar.barPointerPos.x;
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
            Console.SetCursorPosition(fishBar.barPointerPos.x, fishBar.barPointerPos.y);
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.BackgroundColor = ConsoleColor.DarkGray;
            Console.Write("▼");
            Console.ResetColor();
        }

        private void CheckFishingEnd()
        {
            if (fishBar.barCount == fishBar.target)
            {
                game.score++;
                game.ChangeScene(SceneType.Fishery);
            }
        }
    }
}
