using System;
using Business_Data;



namespace Presentation_Business
{

    public delegate void CredentialRequest();

    /// <summary>
    /// Interface to create an authenticator component
    /// </summary>
    public interface IAuthenticator
    {
        /// <summary>
        /// Method that calls the <c>Connect</c> method of the underlying database
        /// </summary>
        /// <returns><c>true</c> if the authentication succeded</returns>
        /// <param name="UserID">The user ID</param>
        /// <param name="UserPassword">The user password</param>
        /// <returns></returns>
        bool Connect(string UserID, string UserPassword);

        
        event CredentialRequest PullCredential;

    }
}