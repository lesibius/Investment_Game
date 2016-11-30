using System;



namespace FinanceLib.Measurement
{
    public class Performance
    {
        /// <summary>
        /// Create a new <c>Performance</c> instance
        /// </summary>
        /// <param name="beginDate">Begin date for the performance measurement</param>
        /// <param name="endDate">End date for the performance measurement</param>
        /// <param name="val">Actual value of the performance as measured between the begin date and the end date</param>   
        public Performance(DateTime beginDate, DateTime endDate, double val)
        {
            BeginDate = beginDate;
            EndDate = endDate;
            Value = val;
        }

        /// <summary>
        /// Date for the begining of the performance measurement
        /// </summary>
        public DateTime BeginDate { get; private set; }

        /// <summary>
        /// Date for the end of the performance measurement
        /// </summary>
        public DateTime EndDate { get; private set; }

        /// <summary>
        /// Performance between <c>BeginDate</c> and <c>EndDate</c>
        /// </summary>
        public double Value { get; private set; }

    }
}