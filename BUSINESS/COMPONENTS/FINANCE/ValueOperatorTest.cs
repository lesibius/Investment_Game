using System;
using ValueOperator;


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


        
        Value x = new Value(1200,"EUR");
        Value y = new Value(125400,"JPY");
        Value z = new Value(1200,"USD");
        Console.WriteLine("{0} - {1} + {2} = {3}",x,y,z,x-y+z);

    }
}