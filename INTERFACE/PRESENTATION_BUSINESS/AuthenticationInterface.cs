using System;
using Business_Data;



namespace Presentation_Business
{
    /// <summary>
    /// Static class to store delegates related to authentication and create authentication events
    /// </summary>
    public static class AuthenticationComponents
    {
        /// <summary>
        /// Delegate to pull credential request
        /// </summary>
        /// <returns>The user ID</returns>
        public delegate Credential PullUserCredentials();

        /// <summary>
        /// Structure to store user credentials
        /// </summary>
        public struct Credential
        {
            public string ID;
            public string PWD;

            /// <summary>
            /// Constructor for the <c>Credential</c> structure
            /// </summary>
            /// <param name="id"></param>
            /// <param name="pwd"></param>
            public Credential(string id, string pwd)
            {
                ID = id;
                PWD = pwd;
            }
        }

    }
    

    /// <summary>
    /// Interface to create an authenticator component
    /// </summary>
    public interface IAuthenticator
    {
        /// <summary>
        /// Method that calls the <c>Connect</c> method of the underlying database
        /// </summary>
        /// <returns><c>true</c> if the authentication succeded</returns>
        bool Connect();

        /// <summary>
        /// Event to trigger pull request for user ID and password
        /// </summary>
        event AuthenticationComponents.PullUserCredentials CredentialRequest;


    }
}