using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Animation
{
    interface IView
    {
        void DisplayString(string str);

        int GetButtonIndex(object obj);

        void SwapState();

        event EventHandler<EventArgs> btnPressed;

        event EventHandler<EventArgs> onBtnPressed;



    }
}
