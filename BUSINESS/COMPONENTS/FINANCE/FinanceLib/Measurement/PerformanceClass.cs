using System;
using FinanceLib.ValueOperator;



namespace FinanceLib.Measurement
{
    public class PerformanceElement
    {
         
        public PerformanceElement(DateTime Date, Value marketValue, Value externalCashFlow, PerformanceElement previous = null)
        {
            this.Date = Date;
            this.MarketValue = marketValue;
            this.ExternalCashFlow = externalCashFlow;
            this.Next = null;
            if(previous != null)
            {
                previous.AppendPerformanceElement(this);
            }
        }

        public PerformanceElement(DateTime Date, Value marketValue)
        {
            this.Date = Date;
            this.MarketValue = marketValue;
            this.ExternalCashFlow = new Value(0,marketValue.Currency);
            this.Previous = null;
            this.Next = null;
        }

        /// <summary>
        /// Measurement date for the performance
        /// </summary>
        public DateTime Date { get; private set; }

        /// <summary>
        /// Market value of the object on the measurement date
        /// </summary>
        public Value MarketValue { get; private set; }

        /// <summary>
        /// Value of external cash flow on the measurement date
        /// </summary>
        /// <returns></returns>
        public Value ExternalCashFlow { get; private set; }

        public PerformanceElement Previous { get; private set; }

        public PerformanceElement Next { get; private set; }


        public void AppendPerformanceElement(PerformanceElement next)
        {
            this.Next = next;
        }

        public override string ToString()
        {
            return Date.ToString() + " - MV: " + MarketValue.ToString() + " - External CF: " + ExternalCashFlow.ToString();
        }

    }
}