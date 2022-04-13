namespace OAST.DemandAllocation.Links
{
    public class Link : ILink
    {
        public int LinkId { get; set; }
        public int StartNodeId { get; set; }
        public int EndNodeId { get; set; }
        public int NumberOfFibrePairs { get; set; }
        public float FibrePairCost { get; set; }
        public int NumberOfLambdasInFibre { get; set; }
        public int LinkCapacity { get; set; }

        public Link(int linkId, 
            int startNodeId, 
            int endNodeId, 
            int numberOfFibrePairs, 
            float fibrePairCost, 
            int numberOfLambdasInFibre)
        {
            LinkId = linkId;
            StartNodeId = startNodeId;
            EndNodeId = endNodeId;
            NumberOfFibrePairs = numberOfFibrePairs;
            FibrePairCost = fibrePairCost;
            NumberOfLambdasInFibre = numberOfLambdasInFibre;
            LinkCapacity = NumberOfFibrePairs * NumberOfLambdasInFibre; // np. Gbps
        }
    }
}