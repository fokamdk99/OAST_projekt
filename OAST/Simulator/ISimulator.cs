namespace OAST.Simulator
{
    public interface ISimulator
    {
        void Run(int queueSize, int numberOfRepetitions, int lambda, int mi);
        void Calculate();
    }
}