using System.Collections.Generic;

namespace OAST.DemandAllocation.EvolutionAlgorithm
{
    public class ChromosomeComparer : IComparer<Chromosome>
    {
        public int Compare(Chromosome? x, Chromosome? y)
        {
            if (ReferenceEquals(x, y)) return 0;
            if (ReferenceEquals(null, y)) return 1;
            if (ReferenceEquals(null, x)) return -1;
            
            return x.MaxLoad.CompareTo(y.MaxLoad);
        }
    }
}