using System.Collections.Generic;

namespace OAST.DemandAllocation.Demands
{
    public class Demand : IDemand
    {
        public int DemandId { get; set; }
        public int StartNodeId { get; set; }
        public int EndNodeId { get; set; }
        public int DemandVolume { get; set; }
        public int NumberOfDemandPaths { get; set; }
        public List<DemandPath> DemandPaths { get; set; }
        
        public Demand(int demandId, 
            int startNodeId, 
            int endNodeId, 
            int demandVolume, 
            int numberOfDemandPaths)
        {
            DemandId = demandId;
            StartNodeId = startNodeId;
            EndNodeId = endNodeId;
            DemandVolume = demandVolume;
            NumberOfDemandPaths = numberOfDemandPaths;
            DemandPaths = new();
        }
    }
}