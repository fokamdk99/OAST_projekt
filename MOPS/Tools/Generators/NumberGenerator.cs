using System;

namespace MOPS.Tools.Generators
{
    public class NumberGenerator : INumberGenerator
    {
        public int Generate(SourceType sourceType, int seed, int lambda)
        {
            Random rnd = new Random(seed);
            int randPoisson = 0; //Poisson variable
            if (sourceType == SourceType.Poisson)
            {
                double exp_lambda = Math.Exp (-lambda); //constant for terminating loop
                double randUni; //uniform variable
                double prodUni; //product of uniform variables
                
 
//initialize variables
                randPoisson = -1;
                prodUni = 1;
                do {
                    randUni = rnd.NextDouble(); //generate uniform variable
                    prodUni = prodUni * randUni; //update product
                    randPoisson++; // increase Poisson variable
 
                } while (prodUni > exp_lambda);

            }
            return randPoisson;
        }
    }
}