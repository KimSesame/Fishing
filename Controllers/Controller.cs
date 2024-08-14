namespace Fishing.Controllers
{
    public abstract class Controller
    {
        protected Game game = Game.game;

        public abstract void Move();
    }
}
