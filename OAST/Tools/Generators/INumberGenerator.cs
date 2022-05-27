namespace OAST.Tools.Generators
{
    public interface INumberGenerator
    {
        double GetInterval(SourceType sourceType, double lambda);
        void SetSeed(int seed);
    }
}