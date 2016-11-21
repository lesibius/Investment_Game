using System;

namespace Presentation.UI.CMD
{
    
    public abstract class CommandPage
    {
        public abstract void Show();

        public CommandPage CallingPage { get; set; }
    }

}