namespace Fishing
{
    public class Fish
    {
        public Point Position { get; set; }
        public char Rprsn { get; set; }
        public int Patience { get; private set; }

        public Fish(int x, int y)
        {
            Position = new Point(x, y);
            Rprsn = '★';
            Patience = Util.MakeRandomInt(3, 6);
        }

        public void Bite()
        {
            // Call FishingScene
            Console.Beep(1000, 500);
            Game.game.ChangeScene(SceneType.Fishing);
        }
    }
}
