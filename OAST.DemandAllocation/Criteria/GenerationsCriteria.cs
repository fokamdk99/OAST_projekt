namespace OAST.DemandAllocation.Criteria
{
    public class GenerationsCriteria
    {
        public int CurrentGeneration { get; set; }
        public int NumberOfGenerations { get; set; }

        public GenerationsCriteria(int numberOfGenerations)
        {
            CurrentGeneration = 0;
            NumberOfGenerations = numberOfGenerations;
        }
    }

    public static class EvaluateGenerationsCriteria
    {
        public static bool Evaluate(GenerationsCriteria criteria)
        {
            return criteria.CurrentGeneration == criteria.NumberOfGenerations;
        }
    }
}