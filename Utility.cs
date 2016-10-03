namespace Conway_Game_of_Life
{
    class Utility
    {
        public static int mod (int x, int m) // x % m
        {
            return ((x % m) + m) % m;
        }
    }
}
