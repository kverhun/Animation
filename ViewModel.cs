using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows.Controls;

namespace Animation
{
    class ViewModel
    {
        public ViewModel(IView view)
        {
            model = new Model();
            this.view = view;
            view.btnPressed += btnPressed;
            view.onBtnPressed += onBtnPressed;
        }


        private void btnPressed(object sender, EventArgs e)
        {
            //Button btn = sender as Button;
            //int num = buttons.IndexOf(btn);
            int btnIndex = view.GetButtonIndex(sender);
            if (btnIndex < 0)
                return;
            else if (btnIndex < 10)
                model.AcceptStr(btnIndex.ToString());
            else if (btnIndex < 16)
            {
                switch (btnIndex)
                {
                    case 10:
                        model.AcceptStr("+");
                        break;
                    case 11:
                        model.AcceptStr("-");
                        break;
                    case 12:
                        model.AcceptStr("*");
                        break;
                    case 13:
                        model.AcceptStr("/");
                        break;
                    case 14:
                        model.AcceptStr("=");
                        break;
                    case 15:
                        model.Clear();
                        break;
                }
            }
            view.DisplayString(model.DisplayString());

        }

        private void onBtnPressed(object sender, EventArgs e)
        {
            view.SwapState();
        }


        private IView view;
        private Model model;


    }
}
