namespace Fishing.Achievements
{
    public class CountAchievement : Achievement
    {
        int count;

        public CountAchievement(int count)
        {
            this.count = count;
            Game.game.OnCountChanged += CheckAchievement;
        }

        public override void CheckAchievement()
        {
            if (Game.game.FishCount >= count)
            {
                Console.WriteLine("Unlock!");
                Thread.Sleep(2000);
                Game.game.OnCountChanged -= CheckAchievement;
            }
        }
    }
}
