namespace Fishing.Achievements
{
    public class ScoreAchievement : Achievement
    {
        public ScoreAchievement()
        {
            Game.game.OnScoreChanged += CheckAchievement;
        }

        public override void CheckAchievement()
        {
            if (Game.game.Score > 10)
            {
                Console.WriteLine("Unlock!");
                Thread.Sleep(2000);
            }
        }
    }
}
