using Fishing.Scenes;

namespace Fishing
{
    public class Game
    {
        public static Game game = new Game();
        
        public bool running;
        public int score;
        public ConsoleKey inputKey;
        public Player player;

        private Scene[] scenes;
        private Scene curScene;

        public Scene CurScene { get { return curScene; } }

        public Game()
        {
            player = new Player();
        }

        public void Run()
        {
            Start();
            while (running)
            {
                Render();
                Input();
                Update();
            }
            End();
        }

        public void ChangeScene(SceneType sceneType)
        {
            curScene.Exit();
            curScene = scenes[(int)sceneType];
            curScene.Enter();
        }

        public void Over()
        {
            Console.Clear();
            running = false;
        }

        public void PrintScore()
        {
            Console.SetCursorPosition(0, 25);
            Console.Write($"Score: {game.score}");
        }

        private void Start()
        {
            // Console configuration
            Console.Title = "FISHing";
            Console.CursorVisible = false;
            Console.SetWindowSize(50, 30);

            // Initialize title scene
            running = true;
            score = 0;

            scenes = new Scene[(int)SceneType.SIZE];
            scenes[(int)SceneType.Title] = new TitleScene();
            scenes[(int)SceneType.Fishery] = new FisheryScene();
            scenes[(int)SceneType.Fishing] = new FishingScene();
            
            curScene = scenes[(int)SceneType.Title];
            curScene.Enter();
        }

        private void Render()
        {
            curScene.Render();
        }

        private void Input()
        {
            curScene.Input();
        }

        private void Update()
        {
            curScene.Update();
        }

        private void End()
        {
            curScene.Exit();
        }
    }
}
