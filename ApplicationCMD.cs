using System;
using CMD;


public class Application
{
    public static void Main()
    {
        MainCommandPage MainPage = new MainCommandPage();
        MainPage.Show();
        MainPage.CallingPage.Show();
    }
}