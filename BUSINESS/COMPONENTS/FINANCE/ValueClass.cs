using System;



namespace ValueOperator
{
    
    /// <summary>
    /// A <c>Value</c> instance store a value in a given currency
    /// </summary>
    public class Value
    {

        /*************************      Constructor     ************************/
        /// <summary>
        /// Constructor for the <c>Value</c> class
        /// </summary>
        /// <param name="unit">Number of unit in the currency</param>
        /// <param name="currency">A <c>Currency</c> instance</param>
        public Value(double unit, Currency currency)
        {
            Unit = unit;
            Currency = currency;
        }

        /// <summary>
        /// Constructor for the <c>Value</c> class
        /// </summary>
        /// <param name="currency">A <c>Currency</c> instance</param>
        public Value(Currency currency)
        {
            Unit = 0;
            Currency = currency;
        }

        /// <summary>
        /// Constructor for the <c>Value</c> class
        /// </summary>
        /// <param name="unit">Number of unit in the currency</param>
        /// <param name="currency">The ISO code of the currency</param>
        public Value(double unit, string currency)
        {
            Unit = unit;
            Currency = Currency.GetCurrency(currency);
        }

        /// <summary>
        /// Constructor for the <c>Value</c> class
        /// </summary>
        /// <param name="currency">the ISO code of the currency</param>
        public Value(string currency)
        {
            Unit = 0;
            Currency = Currency.GetCurrency(currency);
        }


        /*********************      Properties      *******************************/

        /// <summary>
        /// Number of unit expressed in term of the related currency
        /// </summary>
        public double Unit { get; set; }

        /// <summary>
        /// Currency used to express the value
        /// </summary>
        public Currency Currency { get; set; }

        /*********************      Overrided Methods       ***************************/

        /// <summary>
        /// String representation of the <c>Value</c> instance (e.g. EUR 1,250.00)
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return(this.Currency.ISO + " " + String.Format("{0:n}", this.Unit));
        }


        /*********************      Overrided Operators     ******************************/

        /// <summary>
        /// Override the + operator when two <c>Value</c> instances are provided
        /// </summary>
        /// <param name="v1">Left hand side <c>Value</c> (used for target currency</param>
        /// <param name="v2">Right hand side <c>Value</c></param>
        public static Value operator +(Value v1, Value v2)
        {
            if(v1.Currency == v2.Currency)
            {return new Value(v1.Unit + v2.Unit,v1.Currency);}
            else
            {
                return v1;
            }
        }

        /// <summary>
        /// Override the - operator when two <c>Value</c> instances are provided
        /// </summary>
        /// <param name="v1">Left hand side <c>Value</c> (used for target currency)</param>
        /// <param name="v2">Right hand side <c>Value</c></param>
        public static Value operator -(Value v1, Value v2)
        {
            Value tempV = new Value(-v2.Unit, v2.Currency);
            return(v1 + tempV);
        }

        /// <summary>
        /// Override the + operator when a <c>Value</c> instance and a double are provided
        /// </summary>
        /// <param name="v1"><c>Value</c> instance to add (used for target currency)</param>
        /// <param name="vdouble">Double to add</param>
        public static Value operator +(Value v1, double vdouble)
        {
            return new Value(v1.Unit + vdouble,v1.Currency);
        }

        public static Value operator +(double vdouble, Value v1)
        {
            return v1 + vdouble;
        }

        public static Value operator -(Value v1, double vdouble)
        {
            return v1 + (-vdouble);
        }

        public static Value operator -(double vdouble, Value v1)
        {
            return new Value(vdouble - v1.Unit, v1.Currency);
        }
    }
}