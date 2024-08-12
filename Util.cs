namespace Fishing
{
    public static class Util
    {
        public static int MakeRandomInt(int inclusiveMin, int exclusiveMax)
        {
            Random rand = new();
            return rand.Next(inclusiveMin, exclusiveMax);
        }
    }
}
