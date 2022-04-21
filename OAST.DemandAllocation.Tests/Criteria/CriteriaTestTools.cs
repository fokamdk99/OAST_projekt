namespace OAST.DemandAllocation.Tests.Criteria
{
    internal static class CriteriaTestTools
    {
        internal static Parameters GetTestParameters()
        {
            return new Parameters
            {
                CrossoverProbability = 0.7f,
                Mi = 10,
                MutationProbability = 0.3f,
                Seed = 24699,
                StopCriteria = StopCriteriaType.NumberOfGenerations
            };
        }
    }
}