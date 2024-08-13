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
    }
}
