using Fishing.Achievements;
using Fishing.Scenes;

namespace Fishing
{
    public class Game
    {
        public static Game game = new Game();

        public event Action OnScoreChanged;

        public bool running;
        public ConsoleKey inputKey;
        public Player player;
        public Fish fish;

        private AchievementManager achievementManager;
        private Scene[] scenes;
        private Scene curScene;
        private int score;
        private int fishCount;

        public Scene CurScene { get { return curScene; } }
        public int Score
        {
            get { return score; }
            set
            { 
                score = value;
                OnScoreChanged?.Invoke();
            }
        }
        public int FishCount
        {
            get { return fishCount; }
            set
            {
                fishCount = value;
                // OnScoreChanged?.Invoke(value);
            }
        }

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

            // Initialize game data and scenes
            achievementManager = new AchievementManager();

            running = true;
            Score = 0;
            FishCount = 0;

            scenes = new Scene[(int)SceneType.SIZE];
            scenes[(int)SceneType.Title] = new TitleScene();
            scenes[(int)SceneType.Fishery] = new FisheryScene();
            scenes[(int)SceneType.Fishing] = new FishingScene();

            // Subscribe events
            (scenes[(int)SceneType.Fishing] as FishingScene).OnFishCaught += (scenes[(int)SceneType.Fishery] as FisheryScene).CatchFish;

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
