namespace Fishing
{
    public class Player
    {
        public Point Position { get; set; }
        public char Rprsn { get; set; }

        public Player()
        {
            Position = new Point(0, 0);
            Rprsn = 'P';
        }
    }
}
