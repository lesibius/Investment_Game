using System;
using Presentation_Business;
using Business_Data;


namespace Business.Components.DataManagement
{

    public class DBAuthenticator : IAuthenticator
    {
        public DBAuthenticator(IAuthenticable connection)
        {
            this.Connector += connection.Connect;
        }

        public event AuthenticationDelegate.UserIDRetriever PullUserID;
        public event AuthenticationDelegate.UserPasswordRetriver PullUserPassword;

        public delegate bool ConnectMethod(string UserID, string UserPassword);
        ConnectMethod Connector;

        public bool Connect()
        {
            return(Connector("qq","bb"));
        }

    }
}