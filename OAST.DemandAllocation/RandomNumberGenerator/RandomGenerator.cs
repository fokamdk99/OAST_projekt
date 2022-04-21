using System;

namespace OAST.DemandAllocation.RandomNumberGenerator
{
    public class RandomGenerator : IRandomNumberGenerator
    {
        private Random _random;
        public int Seed { get; set; }

        public RandomGenerator()
        {
            Seed = 24699;
            _random = new Random(Seed);
        }
        
        public int GenerateRandomIntNumber(int range)
        {
            var number = _random.Next(0, range);

            return number;
        }

        public float GenerateRandomFloatNumber()
        {
            var number = (float)_random.NextDouble();

            return number;
        }

        public void SetParameters(int seed)
        {
            Seed = seed;
            _random = new Random(Seed);
        }
    }
}