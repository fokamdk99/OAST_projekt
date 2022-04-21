namespace OAST.DemandAllocation.Criteria
{
    public class MutationsCriteria
    {
        public int CurrentMutation { get; set; }
        public int NumberOfMutations { get; set; }

        public MutationsCriteria(int numberOfMutations)
        {
            CurrentMutation = 0;
            NumberOfMutations = numberOfMutations;
        }
    }

    public static class EvaluateMutationsCriteria
    {
        public static bool Evaluate(MutationsCriteria criteria)
        {
            return criteria.CurrentMutation == criteria.NumberOfMutations;
        }
    }
}