namespace Fishing.Achievements
{
    public class ScoreAchievement : Achievement
    {
        int score;

        public ScoreAchievement(int difficulty, int score)
        {
            this.difficulty = difficulty;
            this.score = score;
            Game.game.OnScoreChanged += CheckAchievement;
        }

        public override void CheckAchievement()
        {
            if (Game.game.Score > score)
            {
                PrintUnlockMsg();
                Thread.Sleep(3000);
                Game.game.OnScoreChanged -= CheckAchievement;
            }
        }

        public override void PrintUnlockMsg()
        {
            base.PrintUnlockMsg();

            Console.SetCursorPosition(0, 7);

            Console.SetCursorPosition(0, 8);
            Console.WriteLine($"\t\tScore more than {score}");
            Console.Write("\t\t");
            for (int i = 0; i < difficulty; i++)
                Console.Write("★  ");
        }
    }
}
