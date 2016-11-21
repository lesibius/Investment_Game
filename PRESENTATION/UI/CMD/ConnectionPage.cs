using System;
using Presentation.Interface;


namespace Presentation.UI.CMD
{
    public class CMDConnectionPage : IConnectionDisplay
    {

        public string GetUserID()
        {
            System.Console.WriteLine("Please enter your password");
            return(System.Console.ReadLine());
        }

        public string GetUserPassword()
        {
            System.Console.WriteLine("Please enter your password");
            return(System.Console.ReadLine());
        }

        
    }

}