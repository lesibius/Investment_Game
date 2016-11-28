using System;
using System.Collections.Generic;



namespace ValueOperator
{

    /****************************************************************************************
    *                           Currency Singletons                                         *
    ****************************************************************************************/

    /************************   Currency Management ****************************************/

    /// <summary>
    /// The <c>Currency</c> class allows to manage currencies. It creates singleton of currencies to be used by the program
    /// </summary>
    public sealed class Currency
    {
        /// <summary>
        /// Constructor of <c>Currency</c> singletons
        /// </summary>
        /// <param name="name">Name of the currency</param>
        /// <param name="iso">Code ISO of the currency</param>
        private Currency(string name, string iso)
        {
            Name = name;
            ISO = iso;
        }

        /// <summary>
        /// HashSet of <c>Currency</c> to ensure that each currency is instanciated only once
        /// </summary>
        private static Dictionary<string,Currency> AvailableCurrencies = new Dictionary<string,Currency>();

        /// <summary>
        /// Create an instance of <c>Currency</c> if it has not been instanciated yet and stores it in the HashSet
        /// </summary>
        /// <param name="name"></param>
        /// <param name="iso"></param>
        public static void CreateCurrency(string name, string iso)
        {
            Currency tempCurrency = new Currency(name,iso);
            if(AvailableCurrencies.ContainsValue(tempCurrency))
            {
                Console.WriteLine("Already added");
                throw new System.ArgumentException(
                    "A currency with the ISO code " + tempCurrency.ISO + " is already in the dictionary", "original");
            }
            AvailableCurrencies.Add(iso,tempCurrency);
        }

        /// <summary>
        /// Method to retrieve an instance of a <c>Currency</c>
        /// </summary>
        /// <param name="iso">ISO code of the currency to return</param>
        /// <returns>A currency (or an exception if the currency does not exist)</returns>
        public static Currency GetCurrency(string iso)
        {
            if(AvailableCurrencies.ContainsKey(iso))
            {
                return AvailableCurrencies[iso];
            }
            else
            {
                throw new System.ArgumentException(
                    "No currency with the ISO code " + iso + " has been found" , "original");
            }
        }

        /*****************************      Properties      **************************************/

        /// <summary>
        /// Name of the currency
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// ISO code of the currency
        /// </summary>
        public string ISO { get; set; }


        /****************************       Method Overrided    **************************************/
        /// <summary>
        /// Return a string for the <c>Currency</c> instance
        /// </summary>
        /// <returns>A description of the currency</returns>
        public override string ToString()
        {
            return(this.ISO);
        }

        /// <summary>
        /// Check for <c>Currency</c> equality
        /// </summary>
        /// <param name="obj">Object to compare to the currency</param>
        /// <returns>True of both <c>Currency</c> are equals</returns>
        public override bool Equals(object obj)
        {
            Currency c = obj as Currency;
            if(c == null)                       //If the object is not a Currency, return false
            {
                return false;
            }
            
            //Else return true if the ISO code is the same
            return c.ISO == this.ISO;
        }

        /// <summary>
        /// GetHashCode method
        /// </summary>
        /// <returns>The instance hash code</returns>
        public override int GetHashCode()
        {
            char[] isoLetters = this.ISO.ToCharArray();
            int x = isoLetters[0];
            int y = isoLetters[1];
            int z = isoLetters[2];
            int result = (int) (x ^ (x >> 32));
            result = 31 * result + (int) (y ^ (y >> 32));
            result = 31 * result + (int) (z ^ (z >> 32));
            return result;

            //return Convert.ToInt32(isoLetters[0]);
        }
    }


    /****************************************************************************************
    *                               Currency Pairs                                          *
    ****************************************************************************************/

    /// <summary>
    /// Allows to store a currency exchange rate for currency conversion
    /// </summary>
    public class CurrencyPair
    {   

        /**********************     Constructor     *************************************/


        /// <summary>
        /// Constructor of the <c>CurrencyPair</c> class
        /// </summary>
        /// <param name="baseCurrency">Base currency of the pair</param>
        /// <param name="quotationCurrency">Quotation currency of the pair</param>
        public CurrencyPair(Currency baseCurrency, Currency quotationCurrency)
        {
            if(baseCurrency==quotationCurrency){throw new System.ArgumentException("Quotation and base currency cannot be the same");}
            BaseCurrency = baseCurrency;
            QuotationCurrency = quotationCurrency;
            BID = 0;
            ASK = 0;
            MID = 0;
        }

        /// <summary>
        /// Constructor of the <c>CurrencyPair</c> class
        /// </summary>
        /// <param name="baseCurrency">Base currency of the pair</param>
        /// <param name="quotationCurrency">Quotation currency of the pair</param>
        /// <param name="mid">Mid price of the currency pair</param>
        public CurrencyPair(Currency baseCurrency, Currency quotationCurrency, double mid)
        {
            BaseCurrency = baseCurrency;
            QuotationCurrency = quotationCurrency;
            BID = mid;
            ASK = mid;
            MID = mid;
        }

        /// <summary>
        /// Constructor of the <c>CurrencyPair</c> class
        /// </summary>
        /// <param name="baseCurrency">Base currency of the pair</param>
        /// <param name="quotationCurrency">Quotation currency of the pair</param>
        /// <param name="bid">Bid for the currency pair</param>
        /// <param name="ask">Ask for the currency pair</param>
        public CurrencyPair(Currency baseCurrency, Currency quotationCurrency, double bid, double ask)
        {
            BaseCurrency = baseCurrency;
            QuotationCurrency = quotationCurrency;
            BID = bid;
            ASK = ask;
            MID = (bid + ask) / 2.0;
        }

        /*****************************      Properties      **************************************/

        public Currency BaseCurrency { get; private set;}
        public Currency QuotationCurrency { get; private set;}
        public double BID { get; set; }
        public double ASK { get; set; }
        public double MID { get; private set; }

        /******************************     Overrided Methods   *************************************/

        /// <summary>
        /// Override the <c>ToString</c> method
        /// </summary>
        /// <returns>A <c>string</c> representation of the <c>CurrencyPair</c> instance</returns>
        public override string ToString()
        {
            if(BID != 0 || ASK != 0)
            {
                return(BaseCurrency.ISO + QuotationCurrency.ISO + " " + BID + "/" + ASK);
            }
            else if(MID != 0)
            {
                return(BaseCurrency.ISO + QuotationCurrency.ISO + " " + MID);
            }
            else
            {
                return("No quotation provided for " + BaseCurrency.ISO + QuotationCurrency.ISO);
            }
        }

        /// <summary>
        /// Override the <c>Equals</c> method
        /// </summary>
        /// <param name="obj">Method to test for equality</param>
        /// <returns>True if <c>this</c> instance is equal to <c>obj</c></returns>
        public override bool Equals(object obj)
        {
            return obj is CurrencyPair && this == (CurrencyPair)obj;
        }

        /// <summary>
        /// Override the <c>GetHashCode</c> method
        /// </summary>
        /// <returns>The instance hashcode</returns>
        public override int GetHashCode()
        {
            int x = this.BaseCurrency.GetHashCode();
            int y = this.QuotationCurrency.GetHashCode();
            return Math.Min(x,y) ^ Math.Max(x,y);
        }

        /// <summary>
        /// Override the == operator
        /// </summary>
        /// <param name="c1">Left hand side <c>CurrencyPair</c> instance</param>
        /// <param name="c2">Right hand side <c>CurrencyPair</c> instance</param>
        public static bool operator ==(CurrencyPair c1, CurrencyPair c2)
        {
            bool cond1 = c1.BaseCurrency == c2.BaseCurrency || c1.BaseCurrency == c2.QuotationCurrency;
            bool cond2 = c1.QuotationCurrency == c2.BaseCurrency || c1.QuotationCurrency == c2.QuotationCurrency;
            bool cond3 = (c1.BaseCurrency != c1.QuotationCurrency) && (c2.BaseCurrency != c2.QuotationCurrency);
            return cond1 && cond2 && cond3;
        }

        /// <summary>
        /// Override the != operator
        /// </summary>
        /// <param name="c1">Left hand side <c>CurrencyPair</c> instance</param>
        /// <param name="c2">Right hand side <c>CurrencyPair</c> instance</param>
        public static bool operator !=(CurrencyPair c1, CurrencyPair c2)
        {
            return !(c1==c2);
        }
    }

}