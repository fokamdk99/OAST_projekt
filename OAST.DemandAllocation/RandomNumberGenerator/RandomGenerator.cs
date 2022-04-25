using System;
using System.Collections.Generic;
using System.Linq;

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
        
        public List<int> GenerateWithoutDuplicates(int range)
        {
            List<int> possible = Enumerable.Range(0, range).ToList();
            List<int> listNumbers = new List<int>();
            for (int i = 0; i < range; i++)
            {
                int index = _random.Next(0, possible.Count);
                listNumbers.Add(possible[index]);
                possible.RemoveAt(index);
            }

            return listNumbers;
        }
    }
}