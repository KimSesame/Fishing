using Fishing.Scenes;

namespace Fishing.Controllers
{
    public class RodController : Controller
    {
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
                case ConsoleKey.Escape:
                    game.ChangeScene(SceneType.Fishery);
                    break;
                default:
                    break;
            }
        }

        private void MoveLeft()
        {
            Point next = new Point(game.player.Position.X - 2, game.player.Position.Y);
            if (next.X > 0)
                game.player.Position = next;
        }

        private void MoveRight()
        {
            Point next = new Point(game.player.Position.X + 2, game.player.Position.Y);
            if (next.X < 48)
                game.player.Position = next;
        }
    }
}
