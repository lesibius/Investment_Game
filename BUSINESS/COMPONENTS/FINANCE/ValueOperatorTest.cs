using System;
using FinanceLib.ValueOperator;
using FinanceLib.Investment;


public class program
{
    public static void Main()
    {
        
        Currency.CreateCurrency("Euro","EUR");
        Currency.CreateCurrency("US Dollar","USD");
        Currency.CreateCurrency("Japanese Yen","JPY");

        Currency.SetBIDASK("EUR","USD",1.05,1.07);
        Currency.SetMID("EUR","JPY",118.70);
        Currency.SetMID("USD","JPY",111.86);
        
        Security SomeBond = new Security("2.5% PLAIN VANILLA BOND OF SOME COMPANY","US123123","USD",100000,"USD",102321);
        Position BondPosition = new Position(SomeBond,100);
        Console.WriteLine("Added a new security: {0}",SomeBond);
        Console.WriteLine("{0}: market value = {1}",BondPosition,BondPosition.MarketValue.Convert("JPY"));
    }
}