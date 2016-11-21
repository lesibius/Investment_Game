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
            Model = model;                                          //Set the UI model
            auth.PullUserID += model.GetUserID;                     //Suscribe to the user ID pull request
            auth.PullUserPassword += model.GetUserPassword;         //Suscribe to the user password pull request
        }

        /// <summary>
        /// Model for the connection UI
        /// </summary>
        protected IConnectionDisplay Model { get; set; }

        
        
    }


}