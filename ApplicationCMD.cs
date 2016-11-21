using System;
using Presentation.Interface;
using Presentation.UI.CMD;
using Presentation.Logic;


public class Application
{
    public static void Main()
    {
        CMDConnectionPage CNP = new CMDConnectionPage();
        ConnectionController CNC = new ConnectionController(CNP);
        CNC.GetConnectionInformation();
    }
}