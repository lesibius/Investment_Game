using System;
using FinanceLib.ValueOperator;

namespace Investment_Game.Data
{
    
    public class ExchangeRateHandler : IExchangeRateProvider
    {

        public ExchangeRateHandler(IExchangeRateProvider historicalSource)
        {
            HistoricalSource = historicalSource;
            Today = DateTime.Today;
        }


        public double Convert(string baseCurrency, string quotationCurrency)
        {
            return HistoricalSource.Convert(baseCurrency, quotationCurrency);
        }
        
        public double Convert(string baseCurrency, string quotationCurrency, DateTime date)
        {
            return HistoricalSource.Convert(baseCurrency, quotationCurrency, date);
        }

        private IExchangeRateProvider HistoricalSource { get; set; }
        private DateTime Today { get; set; }

    }



}