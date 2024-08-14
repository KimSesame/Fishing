namespace Fishing.Achievements
{
    public class ScoreAchievement : Achievement
    {
        int score;

        public ScoreAchievement(int score)
        {
            this.score = score;
            Game.game.OnScoreChanged += CheckAchievement;
        }

        public override void CheckAchievement()
        {
            if (Game.game.Score > score)
            {
                Console.WriteLine("Unlock!");
                Thread.Sleep(2000);
                Game.game.OnScoreChanged -= CheckAchievement;
            }
        }
    }
}
