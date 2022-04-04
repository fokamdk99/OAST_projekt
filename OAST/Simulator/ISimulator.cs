using System.Collections.Generic;

namespace OAST.Simulator
{
    public interface ISimulator
    {
        void Run(int queueSize, int numberOfRepetitions, int lambda, int mi);
        void Calculate();
        List<double> CreateRange(double start, double stop, int numberOfPoints);
    }
}