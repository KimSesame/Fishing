namespace Fishing.Achievements
{
    public abstract class Achievement
    {
        public int difficulty;

        public abstract void CheckAchievement();
        public virtual void PrintUnlockMsg()
        {
            Console.SetCursorPosition(0, 5);
            Console.WriteLine("     ┌─────────────────────────────────────┐");
            Console.WriteLine("     │                                     │");
            Console.WriteLine("     │     =Achievement Unlock!=           │");
            Console.WriteLine("     │                                     │");
            Console.WriteLine("     │                                     │");
            Console.WriteLine("     │                                     │");
            Console.WriteLine("     └─────────────────────────────────────┘");
        }
    }
}
