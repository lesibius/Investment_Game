using System;


namespace Presentation.UI.CMD
{

    public class MainCommandPage : CommandPage
    {
        public MainCommandPage()
        {
            System.Console.WriteLine("Welcome to the investment game");
            CallingPage = this;
        }


        public override void Show()
        {
            int selection;
            selection = this.MakeSelection();
            System.Console.WriteLine("The user selected this number: {0}",selection);
        }


        public int MakeSelection()
        {
            int selection;
            try
            {
                selection = Convert.ToInt32(System.Console.ReadLine());
            }
            catch (System.Exception e)
            {
                System.Console.WriteLine("Wrong selection: {0}", e.ToString());
                return(0);
            }
            
            return(selection);
        }

        public new CommandPage CallingPage { get; set; }
    }

}
