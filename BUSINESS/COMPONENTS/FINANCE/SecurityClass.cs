using System;
using FinanceLib.ValueOperator;



namespace FinanceLib.Investment
{

    /// <summary>
    /// Basis class for securities
    /// </summary>
    public class Security
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
    }
}