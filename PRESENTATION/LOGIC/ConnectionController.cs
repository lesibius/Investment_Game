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
            Model = model;                                              //Set the UI model
            Authenticator = auth;                                       //Set the related authenticator
            Model.ValidateCredential += Authenticator.Connect;          //Suscribe to the user ID pull request
            Authenticator.PullCredential += this.Show;                  //When the pull credential event is triggered, the GUI directly request a connection to the database
        }
 
        /// <summary>
        /// Model for the connection UI
        /// </summary>
        protected IConnectionDisplay Model { get; set; }
        
        protected IAuthenticator Authenticator {get; set; }
       
       public void Show()
        {
            Model.Show();
        }
        
    }


}