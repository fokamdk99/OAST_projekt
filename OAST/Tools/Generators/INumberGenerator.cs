namespace OAST.Tools.Generators
{
    public interface INumberGenerator
    {
        int Generate(SourceType sourceType, int seed, int lambda);
    }
}