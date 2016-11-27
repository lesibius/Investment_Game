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
        public static void CreateCurrencyInstance(string name, string iso)
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
        public static Currency GetCurrencyInstance(string iso)
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
}