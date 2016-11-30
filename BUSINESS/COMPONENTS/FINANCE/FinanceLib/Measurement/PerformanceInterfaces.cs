using System;
using FinanceLib.ValueOperator;
using FinanceLib.Investment;



namespace FinanceLib.Measurement
{
    
    public interface IMarketMeasurable
    {
        
        Value MarketValue { get; }

    }

}