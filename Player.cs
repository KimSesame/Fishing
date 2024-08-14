using Fishing.Controllers;

namespace Fishing
{
    public class Player
    {
        public Point Position { get; set; }
        public char Rprsn { get; set; }

        public Controller pController { get; private set; }

        public Player()
        {
            Position = new Point(0, 0);
            Rprsn = 'P';
        }

        public void Enter(SceneType sceneType)
        {
            switch (sceneType)
            {
                case SceneType.Fishery:
                    Position.X = 24;
                    Position.Y = 19;
                    Rprsn = '▲';
                    pController = new PlayerController();
                    break;
                case SceneType.Fishing:
                    Position.X = 24;
                    Position.Y = 26;
                    Rprsn = '▼';
                    pController = new RodController();
                    break;
                default:
                    break;
            }
        }
    }
}
