using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Animation
{
    class Model
    {
        public Model()
        {
            currentState = State.GettingFirst;
            arg1str = "";
            arg2str = "";
            resultStr = "";
            displayStr = "";
        }

        public void Clear()
        {
            currentState = State.GettingFirst;
            arg1str = "";
            arg2str = "";
            resultStr = "";
            displayStr = "";
        }

        public string DisplayString()
        {
            return displayStr;
        }

        private string displayStr;

        private void Append(string str)
        {
            switch (currentState)
            {
                case State.GettingFirst:
                    arg1str += str;
                    break;
                case State.GettingSecond:
                    arg2str += str;
                    break;
                default:

                    break;
            }
        }

        public void AcceptStr(string str)
        {
            switch (currentState)
            {
                case State.GettingFirst:
                    if (char.IsDigit(str[0]))
                        Append(str);
                    if (str[0] == '+' || str[0] == '-' || str[0] == '*' || str[0] == '/')
                    {
                        operChar = str[0];
                        currentState = State.GettingSecond;
                        displayStr = arg2str;
                    }
                    else
                        displayStr = arg1str;
                    break;
                case State.GettingSecond:
                    if (char.IsDigit(str[0]))
                        Append(str);
                    if (str[0] == '=')
                    {
                        currentState = State.ResultLayout;
                        GetResult();
                        displayStr = resultStr;
                    }
                    else
                        displayStr = arg2str;
                    break;
                case State.ResultLayout:
                    if (char.IsDigit(str[0]))
                    {
                        CurrentState = State.GettingFirst;
                        Append(str);
                        displayStr = arg1str;
                    }
                    break;
            }
        }

        public string arg1str
        {
            get;
            private set;
        }

        public string arg2str
        {
            get;
            private set;
        }

        public string resultStr
        {
            get;
            private set;
        }

        private void ClearArgs()
        {
            arg1str = "";
            arg2str = "";
            resultStr = "";
        }

        private char operChar;

        private enum State
        {
            GettingFirst,
            GettingSecond,
            ResultLayout
        }

        private State currentState;
        
        private State CurrentState
        {
            get { return currentState; }

            set
            {
                currentState = value;
                if (currentState == State.GettingFirst)
                    ClearArgs();
            }
        }


        private void GetResult()
        {
            int arg1 = int.Parse(arg1str);
            int arg2 = int.Parse(arg2str);
            int res = 0;
            switch (operChar)
            {
                case '+':
                    GetRes = (x, y) => x + y;
                    break;
                case '-':
                    GetRes = (x, y) => x - y;
                    break;
                case '*':
                    GetRes = (x, y) => x * y;
                    break;
                case '/':
                    GetRes = (x, y) => x / y;
                    break;                    
            }
            resultStr = GetRes(arg1, arg2).ToString();
        }

        private Func<int, int, int> GetRes = null;


    }
}
