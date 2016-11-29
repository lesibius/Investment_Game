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
        Currency.CreateCurrency("Swiss Franc","CHF");

        Currency.SetBIDASK("EUR","USD",1.05,1.07);
        Currency.SetMID("EUR","JPY",118.70);
        Currency.SetMID("USD","JPY",111.86);
        Currency.SetMID("EUR","CHF",1.08);
        Currency.SetMID("USD","CHF",1.01);
        Currency.SetMID("CHF","JPY",111.10);
        
        Security SomeBond = new Security("2.5% PLAIN VANILLA BOND OF SOME COMPANY","US123123","USD",1000,"USD",1023.21);
        Security SomeEquity = new Security("SOME A CLASS SHARE","CH1212","CHF",100,"CHF",100);
        Position BondPosition = 50 * SomeBond;
        Portfolio p = new Portfolio("CHF",BondPosition);
        p.AddPosition(20 * SomeEquity);
        Console.WriteLine("Number of positions in the portfolio: {0}",p.NumberOfPositions);
        Console.WriteLine("Market value of the portfolio: {0}",p.MarketValue);
    }
}