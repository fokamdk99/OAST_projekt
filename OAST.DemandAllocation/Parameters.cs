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
        public int StopValue { get; set; }

        public string DescribeParameters()
        {
            string output = "";

            output += $"IsDap: {IsDap.ToString()}\n" +
                      $"Mi: {Mi}\n" +
                      $"Crossover probability: {CrossoverProbability}\n" +
                      $"Mutation probability: {MutationProbability}\n" +
                      $"Seed: {Seed}\n" +
                      $"Stop criteria type: {StopCriteria.ToString()}\n" +
                      $"Stop value: {StopValue}\n\n" ;
            
            return output;
        }
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