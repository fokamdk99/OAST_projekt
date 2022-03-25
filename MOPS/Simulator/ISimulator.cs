namespace MOPS.Simulator
{
    public interface ISimulator
    {
        void Run(int queueSize, int serverBitRate, int numberOfRepetitions, int lambda);
    }
}