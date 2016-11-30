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
            SetMIDfromBIDASK(0,0);
            IsBIDASK = false;
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
            SetAllRatesToMID(mid);
            SwitchToMID();
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
            SetMIDfromBIDASK(bid,ask);
            SwitchToBIDASK();
        }

        /*****************************      Properties      **************************************/
        /// <summary>
        /// Base currency of the pair
        /// </summary>
        public Currency BaseCurrency { get; private set;}

        /// <summary>
        /// Quotation currency of the pair
        /// </summary>
        public Currency QuotationCurrency { get; private set;}

        /// <summary>
        /// If true, the BID-ASK values are used to retrive the exchange rate
        /// </summary>
        private bool IsBIDASK { get; set; }

        /// <summary>
        /// BID value of the exchange rate
        /// </summary>
        /// <returns></returns>
        public double BID { get; private set; }

        /// <summary>
        /// ASK value of the exchange rate
        /// </summary>
        public double ASK { get; private set; }

        /// <summary>
        /// MID value of the exchange rate
        /// </summary>
        public double MID { get; private set; }

        /**************************      Reverse Currency Pair      *******************************/

        /// <summary>
        /// Create a new <c>CurrencyPair</c> instance with reversed values, without modifying the original instance
        /// </summary>
        /// <returns>A reversed <c>CurrencyPair</c></returns>
        public CurrencyPair GetNewReversedCurrencyPair()
        {
            //Create a new <c>CurrencyPair</c> instance with reversed currencies
            CurrencyPair tempPair = new CurrencyPair(this.QuotationCurrency, this.BaseCurrency);
            //Reverse rates
            if(this.BID != 0 && this.ASK != 0){tempPair.SetMIDfromBIDASK(1.0/this.ASK,1.0/this.BID);}
            else if(this.MID != 0){tempPair.SetMIDOnly(1.0/this.MID);}
            if(this.IsBIDASK){tempPair.SwitchToBIDASK();}
            return tempPair;
        }

        /// <summary>
        /// Reverse <c>this</c> instance of <c>CurrencyPair</c>
        /// </summary>
        public void ReverseThisCurrencyPair()
        {
            //Exchange quotation and base currency
            Currency tempCurrency;
            tempCurrency = this.QuotationCurrency;
            this.QuotationCurrency = this.BaseCurrency;
            this.BaseCurrency = tempCurrency;
            //Reverse rates
            double tempAsk = 1/this.BID;
            if(this.BID != 0){this.BID = 1/this.ASK;}
            if(this.ASK != 0){this.ASK = 1/tempAsk;}
            if(this.MID != 0){this.MID = 1/this.MID;}   //Warning: rounding precision might be an issue here
        }

        /**********************      BID-ASK and MID Mode Management     *************************/

        /// <summary>
        /// Set the exchange rate mode to the MID rate
        /// </summary>
        public void SwitchToMID()
        {
            IsBIDASK = false;
        }

        /// <summary>
        /// Set the exchange rate mode to BID-ASK. Warning: if no BID-ASK spread has been provided, use the MID rate
        /// </summary>
        public void SwitchToBIDASK()
        {
            IsBIDASK = true;
        }



        /***************************        Set Exchange Rates      ******************************/

        /// <summary>
        /// Set the BID, ASK and MID exchange rates
        /// </summary>
        /// <param name="bid">BID exchange rate</param>
        /// <param name="ask">ASK exchange rate</param>
        public void SetMIDfromBIDASK(double bid, double ask)
        {
            BID = bid;
            ASK = ask;
            MID = (bid+ask)/2.0;
        }

        /// <summary>
        /// Set the MID, ASK and BID rate at the same value
        /// </summary>
        /// <param name="mid">MID exchange rate</param>
        public void SetAllRatesToMID(double mid)
        {
            BID = mid;
            ASK = mid;
            MID = mid;
        }

        /// <summary>
        /// Set the MID rate but keep the ASK and BID at the same value. Set the mode to MID.
        /// </summary>
        /// <param name="mid">MID exchange rate</param>
        public void SetMIDOnly(double mid)
        {
            MID = mid;
            SwitchToMID();
        }

        /*************************      Convert Currency        ***********************************/

        /// <summary>
        /// Convert the value using the <c>CurrencyPair</c> data
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public double Convert(double val)
        {
            if(IsBIDASK)    //If bid-ask mode, use BID value
            {
                return val * BID;
            }
            else            //Else use mid
            {
                return val * MID;
            }
        }

        /******************************     Overrided Methods   *************************************/

        /// <summary>
        /// Override the <c>ToString</c> method
        /// </summary>
        /// <returns>A <c>string</c> representation of the <c>CurrencyPair</c> instance</returns>
        public override string ToString()
        {
            if(IsBIDASK && (BID != 0 || ASK != 0))
            {
                return(BaseCurrency.ISO + QuotationCurrency.ISO + " " + String.Format("{0:0.0000}",BID) + "/" + String.Format("{0:0.0000}",ASK));
            }
            else if(MID != 0)
            {
                return(BaseCurrency.ISO + QuotationCurrency.ISO + " " + String.Format("{0:0.0000}",MID));
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