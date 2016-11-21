using System;


namespace Presentation.Interface
{
    /// <summary>
    /// Interface to create a model to display connection UI
    /// </summary>
    public interface IConnectionDisplay
    {
        /// <summary>
        /// Method to get the user ID
        /// </summary>
        /// <returns>The user ID</returns>
        string GetUserID();

        /// <summary>
        /// Method to get the user password
        /// </summary>
        /// <returns>The user password</returns>
        string GetUserPassword();
    }

}