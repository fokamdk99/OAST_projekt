using System.Collections.Generic;

namespace OAST.DemandAllocation.RandomNumberGenerator
{
    public interface IRandomNumberGenerator
    {
        int GenerateRandomIntNumber(int range);
        float GenerateRandomFloatNumber();
        void SetParameters(int seed);
        List<int> GenerateWithoutDuplicates(int range);
    }
}