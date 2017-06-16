namespace CookWithMeSystem.Common.Generator
{
    public interface IRandomGenerator
    {
        string RandomString(int minLength = 3, int maxLength = 25);

        int RandomNumber(int min, int max);
    }
}
