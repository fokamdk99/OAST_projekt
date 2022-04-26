using System;
using OAST.DemandAllocation.EvolutionAlgorithm;

namespace OAST.DemandAllocation.Output
{
    public interface IOutputSaver
    {
        void SaveResults(Chromosome chromosome, string outputFileName, Parameters? parameters, int numberOfIterations, TimeSpan elapsedTime);
    }
}