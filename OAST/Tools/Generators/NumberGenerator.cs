using System;

namespace OAST.Tools.Generators
{
    public class NumberGenerator : INumberGenerator
    {
        private Random _rnd;

        public NumberGenerator()
        {
            _rnd = new Random(912467);
        }

        public void SetSeed(int seed)
        {
            _rnd = new Random(seed);
        }

        public double GetInterval(SourceType sourceType, int lambda)
        {
            return sourceType == SourceType.Poisson ? -Math.Log(_rnd.NextDouble()) / lambda :
                sourceType == SourceType.Uniform ? lambda : 0;
        }
    }
}