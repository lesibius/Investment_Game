using System;
using FinanceLib.ValueOperator;


namespace FinanceLib.Investment
{
    public class Position
    {
        /********************************************************************************************
        *                                   Constructor                                             *
        ********************************************************************************************/
        
        /// <summary>
        /// Create a new <c>Position</c> instance from a <c>Security</c> instance and a quantity
        /// </summary>
        /// <param name="holding"></param>
        /// <param name="quantity"></param>
        public Position(Security holding, double quantity)
        {
            Holding = holding;
            Quantity = quantity;
        }

        /********************************************************************************************
        *                                   Properties                                              *
        ********************************************************************************************/

        /// <summary>
        /// <c>Security</c> instance of the <c>Position</c>
        /// </summary>
        public Security Holding { get; private set; }

        /// <summary>
        /// Quantity owned of the holding
        /// </summary>
        public double Quantity { get; private set; }

        /// <summary>
        /// Market value of the <c>Position</c> instance
        /// </summary>
        public Value MarketValue
        { 
            get //NB: this creates a new <c>Value</c> instance each time the property is called
            {return this.Quantity * this.Holding.MarketValue; }
        }


        /********************************************************************************************
        *                               Overrided Operators                                         *
        ********************************************************************************************/

        /// <summary>
        /// Return a new <c>Position</c> instance with an increase quantity
        /// </summary>
        /// <param name="p">Left hand side <c>Position</c> instance</param>
        /// <param name="q">Right hand side <c>double</c></param>
        public static Position operator +(Position p, double q)
        {
            return new Position(p.Holding, p.Quantity + q);
        }

        /// <summary>
        /// Return a new <c>Position</c> instance with an increase quantity
        /// </summary>
        /// /// <param name="q">Left hand side <c>double</c></param>
        /// <param name="p">Right hand side <c>Position</c> instance</param>
        public static Position operator +(double q, Position p)
        {
            return p + q;
        }

        /// <summary>
        /// Return a new <c>Position</c> instance with a decreased quantity
        /// </summary>
        /// <param name="p">Left hand side <c>Position</c> instance</param>
        /// <param name="q">Right hand side <c>double</c></param>
        public static Position operator -(Position p, double q)
        {
            return new Position(p.Holding, p.Quantity - q);
        }

        /// <summary>
        /// Return a new <c>Position</c> instance with an decreased quantity
        /// </summary>
        /// /// <param name="q">Left hand side <c>double</c></param>
        /// <param name="p">Right hand side <c>Position</c> instance</param>
        public static Position operator -(double q, Position p)
        {
            return p - q;
        }

        public static Portfolio operator +(Position p1, Position p2)
        {
            Portfolio tempPortfolio = new Portfolio(p1.Holding.NominalValue.Currency, p1);
            tempPortfolio.AddPosition(p2);
            return tempPortfolio;
        }

        /********************************************************************************************
        *                               Overrided Methods                                           *
        ********************************************************************************************/

        /// <summary>
        /// String representation of the <c>Position</c> instance
        /// </summary>
        /// <returns>A string representation of the <c>Position</c> instance</returns>
        public override string ToString()
        {
            return this.Quantity + " x " + this.Holding;
        }
    }
}