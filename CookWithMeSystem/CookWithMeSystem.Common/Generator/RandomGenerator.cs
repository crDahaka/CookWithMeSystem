namespace CookWithMeSystem.Common.Generator
{
    using System;
    using System.Text;

    public class RandomGenerator : IRandomGenerator
    {
        private const string Letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
        private Random random;

        public RandomGenerator()
        {
            this.random = new Random();
        }

        public int RandomNumber(int min, int max)
        {
            return this.random.Next(min, max + 1);
        }

        public string RandomString(int minLength = 3, int maxLength = 25)
        {
            var result = new StringBuilder();
            var length = this.random.Next(minLength, maxLength + 1);

            for (int i = 0; i < length; i++)
            {
                result.Append(Letters[this.random.Next(0, Letters.Length)]);
            }

            return result.ToString();
        }
    }
}
