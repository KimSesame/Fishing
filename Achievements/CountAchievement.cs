namespace Fishing.Achievements
{
    public class CountAchievement : Achievement
    {
        int count;

        public CountAchievement(int difficulty, int count)
        {
            this.difficulty = difficulty;
            this.count = count;
            Game.game.OnCountChanged += CheckAchievement;
        }

        public override void CheckAchievement()
        {
            if (Game.game.FishCount >= count)
            {
                PrintUnlockMsg();
                Thread.Sleep(3000);
                Game.game.OnCountChanged -= CheckAchievement;
            }
        }

        public override void PrintUnlockMsg()
        {
            base.PrintUnlockMsg();

            Console.SetCursorPosition(0, 8);
            Console.WriteLine($"\t\t  Catch {count} fish");
            Console.Write("\t\t");
            for (int i = 0; i < difficulty; i++)
                Console.Write("★  ");
        }
    }
}
