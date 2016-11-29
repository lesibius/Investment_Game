using System;
using System.Collections.Generic;
using FinanceLib.ValueOperator;


namespace FinanceLib.Investment
{
    public class Portfolio
    {

        /************************************************************************************
        *                               Constructor                                         *
        ************************************************************************************/

        /// <summary>
        /// Instanciate an empty <c>Portfolio</c> with a reference currency
        /// </summary>
        /// <param name="referenceCurrency">ISO code of the reference currency</param>
        public Portfolio(string referenceCurrency)
        {
            ReferenceCurrency = Currency.GetCurrency(referenceCurrency);
            Positions = new Dictionary<Security,Position>();
        }

        /// <summary>
        /// Instanciate an empty <c>Portfolio</c> with a reference currency
        /// </summary>
        /// <param name="referenceCurrency"><c>Currency</c> instance of the reference currency</param>
        public Portfolio(Currency referenceCurrency)
        {
            ReferenceCurrency = referenceCurrency;
            Positions = new Dictionary<Security,Position>();
        }

        /// <summary>
        /// Instanciate a <c>Portfolio</c> with one <c>Position</c> instance
        /// </summary>
        /// <param name="referenceCurrency">ISO code of the reference currency</param>
        /// <param name="initPosition">Initial <c>Position</c> to add to the <c>Portfolio</c> instance</param>
        public Portfolio(Currency referenceCurrency, Position initPosition)
        {
            ReferenceCurrency = referenceCurrency;
            Positions = new Dictionary<Security,Position>();
            Positions.Add(initPosition.Holding,initPosition);
        }

        /// <summary>
        /// Instanciate a <c>Portfolio</c> with one <c>Position</c> instance
        /// </summary>
        /// <param name="referenceCurrency"><c>Currency</c> instance of the reference currency</param>
        /// <param name="initPosition">Initial <c>Position</c> to add to the <c>Portfolio</c> instance</param>
        public Portfolio(string referenceCurrency, Position initPosition)
        {
            ReferenceCurrency = Currency.GetCurrency(referenceCurrency);
            Positions = new Dictionary<Security,Position>();
            Positions.Add(initPosition.Holding,initPosition);
        }

        /************************************************************************************
        *                               Properties                                          *
        ************************************************************************************/


        private Dictionary<Security,Position> Positions { get; set; }


        /// <summary>
        /// Reference <c>Currency</c> instance of the <c>Portfolio</c> instance
        /// </summary>
        public Currency ReferenceCurrency { get; private set; }

        /// <summary>
        /// Extension of the <c>Count</c> method of the <c>List</c> class
        /// </summary>
        /// <returns>The number of positions (including when several occurence of the same <c>Position</c> of the same instance are included) in the <c>Portfolio</c> instance</returns>
        public int NumberOfPositions
        {
            get{return Positions.Count;}
        }

        /// <summary>
        /// Market value of the <c>Portfolio</c>
        /// </summary>
        public Value MarketValue
        {
            get
            {
                Value tempValue = new Value(0,ReferenceCurrency);
                foreach (Position pos in Positions.Values)
                {
                    tempValue = tempValue + pos.MarketValue;
                }
                return tempValue;
            }
        }

        /************************************************************************************
        *                            Manage Positions                                       *
        ************************************************************************************/

        /// <summary>
        /// Add a <c>Position</c> instance to the <c>Portfolio</c> instance
        /// </summary>
        /// <param name="position"></param>
        public void AddPosition(Position position)
        {
            if(!Positions.K)
        }

        /************************************************************************************
        *                            Overrided Operators                                    *
        ************************************************************************************/
        /*
        public static Portfolio operator +(Portfolio por, Position pos)
        {
            Portfolio.AddPosition(pos);
        }*/


    }
}