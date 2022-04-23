namespace OAST.DemandAllocation
{
    public class Parameters
    {
        public bool IsDap { get; set; }
        public int Mi { get; set; } // licznosc populacji startowej
        public float CrossoverProbability { get; set; }
        public float MutationProbability { get; set; }
        public int Seed { get; set; }
        public StopCriteriaType StopCriteria {get; set;}
    }
    
    // licznosc populacji startowej
    // prawdopodobienstwo krzyzowania i mutacji
    // wybor kryterium stopu:
    // czas, liczba generacji, liczba mutacji, brak poprawy najlepszego znanego rozwiazania w kolejnych N generacjach
    // zapis trajektorii procesu optymalizacji - sekwencja wartosci najlepszych chromosomow w kolejnych generacjach
    // ziarno dla generatora liczb losowych

    public enum StopCriteriaType
    {
        Time = 1,
        NumberOfGenerations = 2,
        NumberOfMutations = 3,
        BestSolution = 4
    }
}