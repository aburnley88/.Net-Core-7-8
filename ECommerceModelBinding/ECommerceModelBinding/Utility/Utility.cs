namespace ECommerceModelBinding.Utility
{
    public static class Utility
    {
        
        public static int GetRandomNumber(int min = 1, int max = 1000)
        {
            Random rnd = new Random();
            return rnd.Next(min, max);
        }
    }
}
