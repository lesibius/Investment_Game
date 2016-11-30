using System;



namespace FinanceLib.ValueOperator
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

        /********************       Conversion Methods      ***************************/
        /// <summary>
        /// Convert to a new <c>Value</c> instance
        /// </summary>
        /// <param name="quotationCurrency">Currency in which the value instance will be converted</param>
        /// <returns>A new <c>Value</c> instance in the quotation currency</returns>
        public Value Convert(string quotationCurrency)
        {
            if(this.Currency.ISO != quotationCurrency)
            {
                double tempUnit = Currency.Convert(this.Unit,this.Currency.ISO,quotationCurrency);
                return new Value(tempUnit, quotationCurrency);
            }
            else
            {
                return new Value(this.Unit,this.Currency.ISO);
            }
        }

        /// <summary>
        /// Convert to a new <c>Value</c> instance
        /// </summary>
        /// <param name="quotationCurrency">Currency in which the value instance will be converted</param>
        /// <returns>A new <c>Value</c> instance in the quotation currency</returns>
        public Value Convert(Currency quotationCurrency)
        {
            if(this.Currency != quotationCurrency)
            {
                double tempUnit = Currency.Convert(this.Unit,this.Currency,quotationCurrency);
                return new Value(tempUnit, quotationCurrency);
            }
            else
            {
                return new Value(this.Unit,this.Currency);
            }
        }

        /*********************      Overrided Methods       ***************************/

        /// <summary>
        /// String representation of the <c>Value</c> instance (e.g. EUR 1,250.00)
        /// </summary>
        /// <returns>A string representation of the <c>Value</c> instance</returns>
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
                Value tempVal = v2.Convert(v1.Currency);
                return v1 + tempVal;
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

        /// <summary>
        /// Override the + operator when a double and a <c>Value</c> instance are provided
        /// </summary>
        /// <param name="vdouble">Double to add</param>
        /// <param name="v1"><c>Value</c> instance to add (used for target currency)</param>
        public static Value operator +(double vdouble, Value v1)
        {
            return v1 + vdouble;
        }

        /// <summary>
        /// Override the - operator when a <c>Value</c> instance and a double are provided
        /// </summary>
        /// <param name="v1"><c>Value</c> instance</param>
        /// <param name="vdouble">Double instance</param>
        public static Value operator -(Value v1, double vdouble)
        {
            return v1 + (-vdouble);
        }

        /// <summary>
        /// Override the - operator when a double and a <c>Value</c> instance are provided
        /// </summary>
        /// <param name="vdouble">Double instance</param>
        /// <param name="v1"><c>Value</c> instance</param>
        public static Value operator -(double vdouble, Value v1)
        {
            return new Value(vdouble - v1.Unit, v1.Currency);
        }

        /// <summary>
        /// Creates a new <c>Value</c> with the number of <c>Unit</c> multiplied by the double
        /// </summary>
        /// <param name="m">Multiplier</param>
        /// <param name="v"><c>Value</c> instance</param>
        public static Value operator *(double m, Value v)
        {
            return new Value(m*v.Unit,v.Currency);
        }

        /// <summary>
        /// Creates a new <c>Value</c> with the number of <c>Unit</c> multiplied by the double
        /// </summary>
        /// <param name="v"><c>Value</c> instance</param>
        /// <param name="m">Multiplier</param>
        public static Value operator *(Value v, double m)
        {
            return m*v;
        }
    }
}