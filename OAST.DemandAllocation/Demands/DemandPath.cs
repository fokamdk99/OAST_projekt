using System.Collections.Generic;

namespace OAST.DemandAllocation.Demands
{
    public class DemandPath : IDemandPath
    {
        public int DemandPathId { get; set; }
        public List<int> LinkIds { get; set; }
        public int Load { get; set; }

        public DemandPath()
        {
            LinkIds = new();
            Load = 0;
        }

        public DemandPath(int demandPathId, List<int> linkIds)
        {
            DemandPathId = demandPathId;
            LinkIds = linkIds;
        }
    }
}