namespace Fishing.Scenes
{
    public abstract class Scene
    {
        protected Game game = Game.game;

        public SceneType type;

        public abstract void Enter();
        public abstract void Render();
        public abstract void Input();
        public abstract void Update();
        public abstract void Exit();
    }
}
