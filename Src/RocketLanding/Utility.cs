namespace RocketLanding
{
    public static class Utility
    {
        public static string GetKey(int x, int y)
        {
            var key = $"{x.ToString().PadLeft(3, '0')}{y.ToString().PadLeft(3, '0')}";

            return key;
        }
    }
}