using System;
using Business_Data;



namespace Presentation_Business
{
    /// <summary>
    /// Static class to store delegates related to authentication and create authentication events
    /// </summary>
    public static class AuthenticationDelegate
    {
        /// <summary>
        /// Delegate for pull request concerning user ID
        /// </summary>
        /// <returns>The user ID</returns>
        public delegate string UserIDRetriever();

        /// <summary>
        /// Delegate for pull request concerning the user password
        /// </summary>
        /// <returns>The user password</returns>
        public delegate string UserPasswordRetriver();
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
        /// Event to trigger pull request for user ID
        /// </summary>
        event AuthenticationDelegate.UserIDRetriever PullUserID;

        /// <summary>
        /// Event to trigger pull request for user password
        /// </summary>
        event AuthenticationDelegate.UserPasswordRetriver PullUserPassword;

    }
}