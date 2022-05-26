namespace OAST.Tools.Generators
{
    public interface INumberGenerator
    {
        double GetInterval(SourceType sourceType, int lambda);
        void SetSeed(int seed);
    }
}