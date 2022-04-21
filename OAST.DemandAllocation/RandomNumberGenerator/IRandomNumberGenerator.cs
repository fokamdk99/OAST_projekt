namespace OAST.DemandAllocation.RandomNumberGenerator
{
    public interface IRandomNumberGenerator
    {
        int GenerateRandomIntNumber(int range);
        float GenerateRandomFloatNumber();
    }
}