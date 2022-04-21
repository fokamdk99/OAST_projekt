using System;
using System.Collections.Generic;

namespace OAST.DemandAllocation.EvolutionAlgorithm
{
    public interface IEvolutionAlgorithm<T> where T : class
    {
        List<Chromosome> Population { get; set; }
        int Mi { get; set; }
        void SetParams(T parameters);

        void Run(Parameters parameters, Func<T, bool> stopCriteria, Action<T>? setupStopCriteria, Action<T>? disposeStopCriteria);
    }
}