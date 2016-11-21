using System;

namespace Business_Data
{
    /// <summary>
    /// Interface for database that require an authentification
    /// </summary>
    public interface IAuthenticable
    {
        /// <summary>
        /// Method for connection to the database
        /// </summary>
        /// <param name="UserID">The user ID</param>
        /// <param name="UserPassword">The user password</param>
        /// <returns></returns>
        bool Connect(string UserID, string UserPassword);
    }
    
}