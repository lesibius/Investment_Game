using System;



namespace Core_Investment
{
    
    /// <summary>
    /// Abstract class used as a model to securities
    /// </summary>
    public abstract class Security
    {
        /// <summary>
        /// Code of the security (e.g. ISIN)
        /// </summary>
        private string Code { get; set; }

        /// <summary>
        /// Name of the security
        /// </summary>
        private string Name { get; set; }


    }



    /// <summary>
    /// Implementation of the abstract <c>Security</c> class for equities
    /// </summary>
    public class Equity : Security
    {

        ///<summary>
        /// Constructor for the <c>Equity</c> class
        /// </summary>
        /// <param name="code">ISIN</param>
        /// <param name="name">Name of the security (e.g. APPL Share class A)</param>
        /// <param name="companyName"></param>
        public Equity(string code, string name, string companyName)
        {
            Code = code;
            Name = name;
        }

        private string Code {get; set; }
        private string Name {get; set; }

        /// <summary>
        /// Override the <c>ToString</c> method
        /// </summary>
        /// <returns>A string describing the security</returns>
        public override string ToString()
        {
            return(Code + " - " + Name);
        }

    }

}