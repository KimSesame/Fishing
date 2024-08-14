namespace Fishing.Achievements
{
    public class AchievementManager
    {
        List<Achievement> list;

        public AchievementManager()
        {
            list =
            [
                new ScoreAchievement(),
            ];
        }
    }
}
