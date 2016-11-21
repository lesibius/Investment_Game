using System;


namespace Presentation_Business
{
    interface IAuthenticable
    {


        public bool Connect(string UserID, string UserPassword);

        /// <summary>
        /// Delegate used to retrive the user ID
        /// </summary>
        /// <returns>The user ID</returns>
        public delegate string UserIDRetriver();

        /// <summary>
        /// Delegate used to retrive the user password
        /// </summary>
        /// <returns>The user password</returns>
        public delegate string UserPasswordRetriever();

        /// <summary>
        /// Implement the delegate to retrieve the user ID
        /// </summary>
        public UserPasswordRetriever GetUserID;

        /// <summary>
        /// Implement the delegate to retrieve the user password
        /// </summary>
        public UserIDRetriver GetUserPassword;
    }
}