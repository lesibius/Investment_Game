using System;
using Business_Data;



namespace Presentation_Business
{

    public static class AuthenticationDelegate
    {
        public delegate string UserIDRetriever();
        public delegate string UserPasswordRetriver();
    }
    

    public interface IAuthenticator
    {
        bool Connect();
        event AuthenticationDelegate.UserIDRetriever PullUserID;
        event AuthenticationDelegate.UserPasswordRetriver PullUserPassword;

    }
}