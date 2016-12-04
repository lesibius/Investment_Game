using System;



namespace FinanceLib.ValueOperator
{

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