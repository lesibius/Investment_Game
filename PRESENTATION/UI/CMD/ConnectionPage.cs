using System;
using Presentation.Interface;


namespace Presentation.UI.CMD
{
    /// <summary>
    /// Class to create a connection page in command line
    /// </summary>
    public class CMDConnectionPage : IConnectionDisplay
    {
        /// <summary>
        /// Method to implement the <c>IConnectionDisplay</c> interface
        /// </summary>
        /// <returns>The user password</returns>
        public string GetUserPassword()
        {
            System.Console.WriteLine("Please enter your password");
            return(System.Console.ReadLine());
        }

        /// <summary>
        /// Method to implement the <c>IConnectionDisplay</c> interface
        /// </summary>
        /// <returns>The user ID</returns>
        public string GetUserID()
        {
            System.Console.WriteLine("Please enter your ID");
            return(System.Console.ReadLine());
        }

    }

}