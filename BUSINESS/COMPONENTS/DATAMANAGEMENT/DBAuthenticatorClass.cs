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
            this.Connection = connection;                       //Store a reference to the database
        }

        /// <summary>
        /// Reference to the database to connect
        /// </summary>
        protected IAuthenticable Connection;

        /// <summary>
        /// Connect to the database
        /// </summary>
        /// <returns><c>true</c> if the connection succeded</returns>
        /// <param name="UserID">The user ID</param>
        /// <param name="UserPassword">The user password</param>
        /// <returns></returns>
        public bool Connect(string UserID, string UserPassword)
        {   
            Connection.Connect(UserID,UserPassword);
            return(true);
        }

        public event CredentialRequest PullCredential;

        public void NewCredentialRequest()
        {
            PullCredential();
        }

    }
}