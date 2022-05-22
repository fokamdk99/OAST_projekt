namespace OAST.Tools.Generators
{
    public interface INumberGenerator
    {
        double Generate(SourceType sourceType, int seed, int lambda);
    }
}