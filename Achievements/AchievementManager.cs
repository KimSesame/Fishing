namespace Fishing.Achievements
{
    public class AchievementManager
    {
        List<Achievement> list;

        public AchievementManager()
        {
            list =
            [
                new ScoreAchievement(1, 10),
                new ScoreAchievement(2, 100),
                new ScoreAchievement(3, 1000),

                new CountAchievement(1, 1),
                new CountAchievement(2, 10),
                new CountAchievement(3, 100),
            ];
        }
    }
}
