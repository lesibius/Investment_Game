using System;



namespace FinanceLib.Measurement
{
    public class Performance
    {
        
        public Performance(DateTime beginDate, DateTime endDate, double val)
        {
            BeginDate = beginDate;
            EndDate = endDate;
            Value = val;
        }

        public DateTime BeginDate { get; private set; }
        public DateTime EndDate { get; private set; }
        public double Value { get; private set; }

    }
}