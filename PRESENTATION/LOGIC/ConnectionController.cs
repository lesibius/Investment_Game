using System;
using Presentation.Interface;

namespace Presentation.Logic
{
    
    public class ConnectionController
    {

        public ConnectionController(IConnectionDisplay model)
        {
            Model = model;
        }

        protected IConnectionDisplay Model { get; set; }

        public void GetConnectionInformation()
        {
            System.Console.WriteLine(Model.GetUserID());
            System.Console.WriteLine(Model.GetUserPassword());
        }
    }


}