using System;
using System.Collections.Generic;

namespace FinanceLib.Measurement
{

    public class PerformanceMeasurer
    {
        public PerformanceMeasurer(IMarketMeasurable measurable)
        {
            Measurable = measurable;
        }

        public IMarketMeasurable Measurable { get; private set;}

    }

}