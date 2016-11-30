using System;
using System.Collections.Generic;

namespace FinanceLib.Measurement
{

    public class PerformanceMeasurer
    {
        public PerformanceMeasurer(IMarketMeasurable measurable)
        {
            Measurable = measurable;
            PerformanceList = new List<Performance>();
        }

        public IMarketMeasurable Measurable { get; private set;}

        public List<Performance> PerformanceList { get; private set; }


        public void AppendToPerformanceQueue(Performance perf)
        {
            PerformanceList.Add(perf);
        }

    }

}