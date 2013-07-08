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

        event EventHandler<EventArgs> btnPressed;

    }
}
