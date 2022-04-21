using System;
using System.Collections.Generic;

namespace OAST.DemandAllocation.EvolutionAlgorithm
{
    public interface IEvolutionAlgorithm<T> where T : class
    {
        int Iteration { get; set; }
        List<Chromosome> Population { get; set; }
        int Mi { get; set; }
        public int NumberOfIterations { get; set; }
        void SetParams(T parameters);

        void Run(Parameters parameters, Func<T, bool> stopCriteria, Action<T>? setupStopCriteria, Action<T>? disposeStopCriteria);
    }
}