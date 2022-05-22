using System;
using MathNet.Numerics.Distributions;

namespace OAST.Tools.Generators

{
    public class NumberGenerator : INumberGenerator
    {
        public double Generate(SourceType sourceType, int seed, int lambda)
        {
            Random rnd = new Random(seed);
            double w = rnd.NextDouble();
            return (1.0 / -lambda) * Math.Log(w);
        }
    }
}