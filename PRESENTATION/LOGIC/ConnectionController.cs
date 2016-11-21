using System;
using Presentation.Interface;
using Presentation_Business;

namespace Presentation.Logic
{
    
    /// <summary>
    /// Controller for connection UI
    /// </summary>
    public class ConnectionController
    {
        /// <summary>
        /// Constructor of the <c>ConnectionController</c> class
        /// </summary>
        /// <param name="model">A model that implement the <c>IConnectionDisplay</c> interface</param>
        /// <param name="auth">A business components that implement the <c>IAuthenticator</c> interface</param>
        public ConnectionController(IConnectionDisplay model, IAuthenticator auth)
        {
            Model = model;
            this.GetUserID += model.GetUserID;                  //Use the GetUserID as a delegated function
            this.GetUserPassword += model.GetUserPassword;      //Use the GetUserPassword as a delegated function
            auth.GetUserID += model.GetUserID;
        }

        /// <summary>
        /// Model for the connection UI
        /// </summary>
        /// <returns></returns>
        protected IConnectionDisplay Model { get; set; }

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