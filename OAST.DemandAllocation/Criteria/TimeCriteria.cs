using System;
using System.Diagnostics;

namespace OAST.DemandAllocation.Criteria
{
    public class TimeCriteria
    {
        public TimeSpan ElapsedTime { get; set; }
        public TimeSpan MaxDuration { get; set; }
        public Stopwatch Timer { get; set; }

        public TimeCriteria(int maxDuration)
        {
            Timer = new Stopwatch();
            MaxDuration = TimeSpan.FromSeconds(maxDuration);
        }
    }
    
    public static class EvaluateTimeCriteria
    {
        public static void StartTimer(TimeCriteria criteria)
        {
            criteria.Timer.Start();
        }
        
        public static void StopTimer(TimeCriteria criteria)
        {
            criteria.Timer.Stop();
        }
        
        public static bool Evaluate(TimeCriteria criteria)
        {
            return criteria.ElapsedTime > criteria.MaxDuration;
        }
    }
}