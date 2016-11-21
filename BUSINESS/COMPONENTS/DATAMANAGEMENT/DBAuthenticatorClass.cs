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

        public delegate string UserIDRetriever();
        public delegate string UserPasswordRetriever();

        public delegate bool ConnectMethod(string UserID, string UserPassword);
        ConnectMethod Connector;

        public bool Connect()
        {
            return(Connector("qq","bb"));
        }

    }
}