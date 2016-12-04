using System;
using System.Collections.Generic;

namespace FinanceLib.Measurement
{

    public class PerformanceMeasurer
    {
        public PerformanceMeasurer(IMarketMeasurable measurable)
        {
            Measurable = measurable;
            PerformanceList = new List<PerformanceElement>();
        }

        public IMarketMeasurable Measurable { get; private set;}

        public List<PerformanceElement> PerformanceList { get; private set; }


        public void AppendToPerformanceQueue(PerformanceElement perf)
        {
            PerformanceList.Add(perf);
        }

    }

}