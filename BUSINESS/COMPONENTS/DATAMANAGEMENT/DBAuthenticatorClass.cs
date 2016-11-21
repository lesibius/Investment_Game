using System;
using Presentation_Business;
using Business_Data;


namespace Business.Components.DataManagement
{
    /// <summary>
    /// Component for DB authentication
    /// </summary>
    public class DBAuthenticator : IAuthenticator
    {
        /// <summary>
        /// Constructor of the <c>DBAuthenticator</c> class
        /// </summary>
        /// <param name="connection">Instance implementing the <c>IAuthenticable</c> interface</param>
        public DBAuthenticator(IAuthenticable connection)
        {
            this.Connector += connection.Connect;
        }

        /// <summary>
        /// Event triggering a pull request for the user ID
        /// </summary>
        public event AuthenticationDelegate.UserIDRetriever PullUserID;

        /// <summary>
        /// Event triggering a pull request for the user password
        /// </summary>
        public event AuthenticationDelegate.UserPasswordRetriver PullUserPassword;

        /// <summary>
        /// Delegate for database connection. Linked to the <c>Connect</c> method implemented bw the <c>IAuthenticable</c> interface
        /// </summary>
        /// <param name="UserID">User ID to connect to the <c>IAuthenticable</c> instance</param>
        /// <param name="UserPassword">User password to connect to the <c>IAuthenticable</c> instance</param>
        /// <returns><c>true</c> if the connection succeded</returns>
        public delegate bool ConnectMethod(string UserID, string UserPassword);

        /// <summary>
        /// Iplement the <c>ConnectMethod</c> delegate
        /// </summary>
        ConnectMethod Connector;

        /// <summary>
        /// Create two push request for the user ID and password and connect to the database
        /// </summary>
        /// <returns><c>true</c> if the connection succeded</returns>
        public bool Connect()
        {   
            string ID = PullUserID();
            string PWD = PullUserPassword();
            return(Connector(ID,PWD));
        }

    }
}