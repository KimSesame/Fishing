namespace Fishing.Achievements
{
    public class AchievementManager
    {
        List<Achievement> list;

        public AchievementManager()
        {
            list =
            [
                new ScoreAchievement(10),
                new ScoreAchievement(100),
                new ScoreAchievement(1000),

                new CountAchievement(1),
                new CountAchievement(10),
                new CountAchievement(100),
            ];
        }
    }
}
