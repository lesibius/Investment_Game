using System;
using Presentation.Interface;
using Presentation.UI.CMD;
using Presentation.Logic;
using Business.Components.DataManagement;


public class Application
{
    public static void Main()
    {
        CMDConnectionPage CNP = new CMDConnectionPage();
        ConnectionController CNC = new ConnectionController(CNP);
        Console.WriteLine("{0}",CNC.GetUserID());
    }
}