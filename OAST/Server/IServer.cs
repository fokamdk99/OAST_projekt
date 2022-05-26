using System.Collections.Generic;
using OAST.Events;
using OAST.Tools.Generators;

namespace OAST.Server
{
    public interface ICustomServer
    {
        bool Busy { get; set; }
        double BusyStart { set; get; }
        double BusyStop { set; get; }
        int Mi { get; set; }
        List<int> Queue { get; set; }
        void SetMi(int mi);
        void Reset();
        int Get();
    }
}