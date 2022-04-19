using System.Collections.Generic;
using OAST.DemandAllocation.EvolutionAlgorithm;

namespace OAST.DemandAllocation.BruteForceTools
{
    public interface IBfTools
    {
        List<List<int>> Recursion(List<int> vector, int depth, int bandwidth, int length);

        List<List<int>> GetPermutations(
            List<List<int>> listOfLists);

        List<Chromosome> GenerateAllPossibleChromosomes();
    }
}