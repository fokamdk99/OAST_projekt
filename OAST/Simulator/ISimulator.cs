using System.Collections.Generic;

namespace OAST.Simulator
{
    public interface ISimulator
    {
        void Run();
        //List<double> CreateRange(double start, double stop, int numberOfPoints);
    }
}