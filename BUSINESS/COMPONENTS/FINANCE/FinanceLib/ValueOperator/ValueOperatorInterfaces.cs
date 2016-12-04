using System;


namespace FinanceLib.ValueOperator
{

    public interface IExchangeRateProvider
    {
        double Convert(string baseCurrency, string quotationCurrency);

        double Convert(string baseCurrency, string quotationCurrency, DateTime date);
    }


}