using System;
using System.Collections.Generic;



namespace FinanceLib.ValueOperator
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
        /// Dictionary of <c>Currency</c> to ensure that each currency is instanciated only once
        /// </summary>
        private static Dictionary<string,Currency> AvailableCurrencies = new Dictionary<string,Currency>();

        /// <summary>
        /// Dictionary of <c>CurrencyPair</c> to manage exchange rates
        /// </summary>
        private static Dictionary<CurrencyPair, CurrencyPair> Pairs = new Dictionary<CurrencyPair, CurrencyPair>();

        /// <summary>
        /// Create an instance of <c>Currency</c> if it has not been instanciated yet and stores it in the Dictionary
        /// </summary>
        /// <param name="name"></param>
        /// <param name="iso"></param>
        public static void CreateCurrency(string name, string iso)
        {
            //Create a new temporary currency if not already present in the dictionary
            Currency tempCurrency = new Currency(name,iso);
            if(AvailableCurrencies.ContainsValue(tempCurrency))
            {
                Console.WriteLine("Already added");
                throw new System.ArgumentException(
                    "A currency with the ISO code " + tempCurrency.ISO + " is already in the dictionary", "original");
            }
            //Create a new CurrencyPair for each <c>Currency</c> present in the dictionary
            if(AvailableCurrencies.Count > 0)
            {
                foreach (Currency c in AvailableCurrencies.Values)
                {
                    CurrencyPair tempPair = new CurrencyPair(c,tempCurrency);
                    Pairs.Add(tempPair,tempPair);
                }
            }

            //Add it to the dictionary
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
        /***************************        Convert Currency        *******************************/

        /// <summary>
        /// Convert a value from a base currency to a quotation currency
        /// </summary>
        /// <param name="val">Value to convert</param>
        /// <param name="baseCurrency">Base currency</param>
        /// <param name="quotationCurrency">Quotation currency</param>
        /// <returns>A converted value</returns>
        public static double Convert(double val, string baseCurrency, string quotationCurrency)
        {
            Currency keybase = Currency.GetCurrency(baseCurrency);
            Currency keyquote = Currency.GetCurrency(quotationCurrency);
            CurrencyPair tempPair = Pairs[new CurrencyPair(keybase,keyquote)];
            if(tempPair.BaseCurrency != keybase)
            {
                tempPair = tempPair.GetNewReversedCurrencyPair();
            }
            
            return tempPair.Convert(val);
        }

        /// <summary>
        /// Convert a value from a base currency to a quotation currency
        /// </summary>
        /// <param name="val">Value to convert</param>
        /// <param name="baseCurrency">Base currency</param>
        /// <param name="quotationCurrency">Quotation currency</param>
        /// <returns>A converted value</returns>
        public static double Convert(double val, Currency baseCurrency, Currency quotationCurrency)
        {
            return Convert(val,baseCurrency.ISO,quotationCurrency.ISO);
        }

        /********************       Manage Exchange Rates       ***********************************/

        /// <summary>
        /// Set the BID and ASK value for the selected currency pair
        /// </summary>
        /// <param name="baseCurrency">Base currency</param>
        /// <param name="quotationCurrency">Quotation currency</param>
        /// <param name="bid">BID value</param>
        /// <param name="ask">ASK value</param>
        public static void SetBIDASK(string baseCurrency, string quotationCurrency, double bid, double ask)
        {
            //Create the key to retrieve the currency pair
            Currency basekey = Currency.GetCurrency(baseCurrency);
            Currency quotekey = Currency.GetCurrency(quotationCurrency);
            //Retrieve the currency pair
            CurrencyPair tempPair = Pairs[new CurrencyPair(basekey,quotekey)];
            //Reverse the pair if required
            if(tempPair.BaseCurrency != basekey)
            {
                //This does not create a new instance but reverse the actual instance.
                //The idea is that the user will probably always provide the same pair
                tempPair.ReverseThisCurrencyPair();
            }
            //Set values and switch to bid-ask mode
            tempPair.SetMIDfromBIDASK(bid,ask);
            tempPair.SwitchToBIDASK();
        }

        /// <summary>
        /// Set the MID rate for the selected currency pair
        /// </summary>
        /// <param name="baseCurrency">Base currency</param>
        /// <param name="quotationCurrency">Quotation currency</param>
        /// <param name="mid">MID rate to set</param>
        public static void SetMID(string baseCurrency, string quotationCurrency, double mid)
        {
            //Create the key to retrive the currency pair
            Currency basekey = Currency.GetCurrency(baseCurrency);
            Currency quotekey = Currency.GetCurrency(quotationCurrency);
            //Retrieve the currency pair
            CurrencyPair tempPair = Pairs[new CurrencyPair(basekey,quotekey)];
            //Reverse if required
            if(tempPair.BaseCurrency != basekey)
            {
                //This does not create a new instance but reverse the actual instance.
                //The idea is that the user will probably always provide the same pair
                tempPair.ReverseThisCurrencyPair();
            }
            //Set values and switch to MID mode
            tempPair.SetMIDOnly(mid);
            tempPair.SwitchToMID();
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

        }
    }


    
}