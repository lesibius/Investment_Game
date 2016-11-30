using System;
using FinanceLib.ValueOperator;
using FinanceLib.Measurement;



namespace FinanceLib.Investment
{

    /// <summary>
    /// Basis class for securities
    /// </summary>
    public class Security : IMarketMeasurable
    {

        /********************************************************************************
        *                               Constructor                                     *
        ********************************************************************************/

        /// <summary>
        /// Constructor of the <c>Security</c> class
        /// </summary>
        /// <param name="name">Name of the security</param>
        /// <param name="code">Code of the security (e.g. ISIN)</param>
        /// <param name="nominal">Nominal value of the security</param>
        /// <param name="market">Market value of the security</param>
        public Security(string name, string code, Value nominal, Value market)
        {
            Name = name;
            Code = code;
            NominalValue = nominal;
            MarketValue = market;
        }

        /// <summary>
        /// Constructor of the <c>Security</c> class
        /// </summary>
        /// <param name="name">Name of the security</param>
        /// <param name="code">Code of the security (e.g. ISIN)</param>
        /// <param name="nomCurrency">Nominal value currency</param>
        /// <param name="nomUnit">Nominal value expressed in nominal value currency</param>
        /// <param name="markCurrency">Market value currency</param>
        /// <param name="markUnit">Market value expressed in market value currency</param>
        public Security(string name, string code, string nomCurrency, double nomUnit, string markCurrency, double markUnit)
        {
            Name = name;
            Code = code;
            NominalValue = new Value(nomUnit,nomCurrency);
            MarketValue = new Value(markUnit,markCurrency);
        }


        /********************************************************************************
        *                               Properties                                      *
        ********************************************************************************/
        /// <summary>
        /// Name of the <c>Security</c> instance
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Code (e.g. ISIN) of the <c>Security</c> instance
        /// </summary>
        public string Code { get; private set; }

        /// <summary>
        /// Nominal value of the <c>Security</c> instance
        /// </summary>
        public Value NominalValue { get; private set; }

        /// <summary>
        /// Market value of the <c>Security</c> instance
        /// </summary>
        /// <returns></returns>
        public Value MarketValue { get; private set; }

        /********************************************************************************
        *                               Overrided Operators                             *
        ********************************************************************************/
        /// <summary>
        /// Return a new position from the <c>Security</c> instance containing m contracts
        /// </summary>
        /// <param name="m">Quantity of <c>Security</c> in the position</param>
        /// <param name="s"><c>Security</c> in the position</param>
        public static Position operator *(double m, Security s)
        {
            return new Position(s,m);
        }

        /// <summary>
        /// Return a new position from the <c>Security</c> instance containing m contracts
        /// </summary>
        /// <param name="s"><c>Security</c> in the position</param>
        /// <param name="m">Quantity of <c>Security</c> in the position</param>
        public static Position operator *(Security s, double m)
        {
            return m*s;
        }

        /********************************************************************************
        *                               Overrided Methods                               *
        ********************************************************************************/


        public override string ToString()
        {
            return this.Code + " - " + this.Name;
        }
    }
}