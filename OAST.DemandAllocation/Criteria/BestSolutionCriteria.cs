using System;
using System.Collections.Generic;

namespace OAST.DemandAllocation.Criteria
{
    public class BestSolutionCriteria
    {
        public int CurrentBestSolution { get; set; }
        public int NumberOfGenerationsWithoutBetterSolution { get; set; }
        public int Threshold { get; set; }
        public List<int> BestSolutions { get; set; }
        public List<int> GenerationsWithoutBetterSolution { get; set; }

        public BestSolutionCriteria(int threshold)
        {
            Threshold = threshold;
            BestSolutions = new List<int>();
            GenerationsWithoutBetterSolution = new List<int>();
            CurrentBestSolution = Int32.MaxValue;
        }
    }

    public static class EvaluateBestSolutionCriteria
    {
        public static bool Evaluate(BestSolutionCriteria criteria)
        {
            return criteria.NumberOfGenerationsWithoutBetterSolution == criteria.Threshold;
        }
    }
}