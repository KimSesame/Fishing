namespace Fishing.Controllers
{
    public class PlayerController : Controller
    {
        public event Action OnRodThrowed;

        public override void Move()
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
            Point next = new Point(game.player.Position.X - 2, game.player.Position.Y);

            if (next.X > 8)
                game.player.Position = next;
        }

        private void MoveRight()
        {
            Point next = new Point(game.player.Position.X + 2, game.player.Position.Y);

            if (next.X < 40)
                game.player.Position = next;
        }

        private void ThrowRod()
        {
            int x = game.player.Position.X;
            int y = game.player.Position.Y - 1;

            Console.ForegroundColor = ConsoleColor.DarkRed;

            // Nothing happen
            if (!IsFishColumn())
            {
                // Print effect(?)
                Console.SetCursorPosition(x + 2, game.player.Position.Y);
                Console.Write("?");
                Console.Beep(500, 500);
                Console.ResetColor();
                return;
            }

            // Fish bite!
            // Print effect(!)
            Console.SetCursorPosition(x + 2, game.player.Position.Y);
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
            for (; y > game.fish.Position.Y + 1; y--)
            {
                Console.SetCursorPosition(x, y);
                Console.Write("∥");
            }

            // End of fishing rod
            Console.SetCursorPosition(x, y);
            Console.Write("§");
            Console.ResetColor();

            OnRodThrowed?.Invoke();
        }

        private bool IsFishColumn()
        {
            return game.player.Position.X == game.fish.Position.X;
        }
    }
}
