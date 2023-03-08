using MontyHallv2.Interfaces;

namespace MontyHallv2.Random
{
    public class RandomNum : IRandom
    {
        private readonly System.Random _random = new();
        public int GetNumberBetweenRange(int min, int max) => _random.Next(min, max);
    }    
}