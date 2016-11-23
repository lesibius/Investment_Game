//General namespace
using System;
//Presentation layer namespace
using Presentation.Interface;
using Presentation.UI.CMD;
using Presentation.Logic;
//Business layer namespace
using Business.Components.DataManagement;
//Data layer namespace
using Data.AccessComponents;

public class Application
{
    public static void Main()
    {
        AccountingCenterWrapper AC = new AccountingCenterWrapper();
        DBAuthenticator Auth = new DBAuthenticator(AC);
        CMDConnectionPage CNP = new CMDConnectionPage();
        ConnectionController CNC = new ConnectionController(CNP, Auth);

        Auth.NewCredentialRequest();
    }
}