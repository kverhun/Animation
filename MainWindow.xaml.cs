using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using System.Windows.Threading;
using System.Windows.Media.Animation;

namespace Animation
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, IView
    {
        private Random generator;

        public MainWindow()
        {
            
            generator = new Random(10000000);
            InitializeComponent();
            buttons = canvasKeyboard.Children;
            foreach (ContentControl ctrl in buttons)
                ctrl.RenderTransform = new TranslateTransform();            
            labels = panelDisplay.Children;

            this.state = true;
            //SwapState();

            ViewModel vm = new ViewModel(this);

        }


        // controls on "keyboard" will be stored here
        private UIElementCollection buttons;

        // labels on "dislay" will be stored here
        private UIElementCollection labels;

        private void btn_Click(object sender, RoutedEventArgs e)
        {
            btnPressed(sender, e);          
            MixButtons(0.35, 0);   
        }

        private void onBtn_Click(object sender, RoutedEventArgs e)
        {
            SwapState();
        }

        // generates and runs correct pair swapping will all elements
        private void MixButtons(double secDuration, double secWait)
        {
            int count = canvasKeyboard.Children.Count;
            int[] swap = new int[count];
            int[] values = new int[count];
            bool[] done = new bool[count];
            for (int i = 0; i < count; ++i)
                values[i] = i;

            int[] swappers = new int[count / 2];
            int[] swapperValues = new int[swappers.Length];
            for (int i = 0; i < swappers.Length; ++i)
            {
                int current = generator.Next() % (count - i);
                swappers[i] = values[current];
                values[current] = values[count - 1 - i];
            }

            for (int i = 0; i < swapperValues.Length; ++i)
            {
                int current = generator.Next() % (count - i - swappers.Length);
                swapperValues[i] = values[current];
                values[current] = values[count - i - 1 - swappers.Length];
            }

            for (int i = 0; i < swappers.Length; ++i)
            {
                SwapButtons(swappers[i], swapperValues[i], secDuration, secWait + i*(secDuration*1));               
            }
        }

        // runs animation for swapping two controls
        private void SwapButtons(int ind1, int ind2, double secDuration, double secWait)
        {
            // nothing to swap
            if (ind1 == ind2) return;

            ContentControl ctrl1 = buttons[ind1] as ContentControl;
            ContentControl ctrl2 = buttons[ind2] as ContentControl;

            // if something went wrong
            if (ctrl1 == null || ctrl2 == null)
                return;
            
            double secHDuration = secDuration / 2;

            // start positions
            var top1 = Canvas.GetTop(ctrl1);
            var left1 = Canvas.GetLeft(ctrl1);
            var top2 = Canvas.GetTop(ctrl2);
            var left2 = Canvas.GetLeft(ctrl2);


            // offsets
            var offsetX1 = (ctrl1.RenderTransform as TranslateTransform).X;
            var offsetY1 = (ctrl1.RenderTransform as TranslateTransform).Y;
            var offsetX2 = (ctrl2.RenderTransform as TranslateTransform).X;
            var offsetY2 = (ctrl2.RenderTransform as TranslateTransform).Y;

            // actual position
            var acttop1 = top1 + offsetY1;
            var acttop2 = top2 + offsetY2;
            var actleft1 = left1 + offsetX1;
            var actleft2 = left2 + offsetX2;

            // horizontal animation for #1
            DoubleAnimation anim1x = new DoubleAnimation();
            anim1x.By = actleft2 - actleft1;
            anim1x.Duration = new Duration(TimeSpan.FromSeconds(secHDuration));
            anim1x.BeginTime = TimeSpan.FromSeconds(secWait);            
            anim1x.AutoReverse = false;
            
            // vertical animation for #1
            DoubleAnimation anim1y = new DoubleAnimation();
            anim1y.By = acttop2 - acttop1;
            anim1y.Duration = new Duration(TimeSpan.FromSeconds(secDuration));
            anim1y.BeginTime = TimeSpan.FromSeconds(secWait);           
            anim1y.AutoReverse = false;

            // horizontal animation for #2
            DoubleAnimation anim2x = new DoubleAnimation();
            anim2x.By = actleft1 - actleft2;
            anim2x.Duration = new Duration(TimeSpan.FromSeconds(secHDuration));
            anim2x.BeginTime = TimeSpan.FromSeconds(secWait);
            anim2x.AutoReverse = false;

            // vertical animation for #2
            DoubleAnimation anim2y = new DoubleAnimation();
            anim2y.By = acttop1 - acttop2;
            anim2y.Duration = new Duration(TimeSpan.FromSeconds(secDuration));
            anim2y.BeginTime = TimeSpan.FromSeconds(secWait);            
            anim2y.AutoReverse = false;

            // makes different paths if animation is only horisontal
            if (actleft1 == actleft2)
            {
                anim1x.By = 30;
                anim1x.AutoReverse = true;
                anim1x.Duration = new Duration(TimeSpan.FromSeconds(secHDuration));

                anim2x.By = -30;
                anim2x.AutoReverse = true;
                anim2x.Duration = new Duration(TimeSpan.FromSeconds(secHDuration));
            }

            // makes different paths if animation is only vertical
            if (acttop1 == acttop2)
            {
                anim1y.By = 30;
                anim1y.AutoReverse = true;
                anim1y.Duration = new Duration(TimeSpan.FromSeconds(secHDuration));

                anim2y.By = -30;
                anim2y.AutoReverse = true;
                anim2y.Duration = new Duration(TimeSpan.FromSeconds(secHDuration));
            }

            // starting animations
            ctrl1.RenderTransform.BeginAnimation(TranslateTransform.XProperty, anim1x);
            ctrl1.RenderTransform.BeginAnimation(TranslateTransform.YProperty, anim1y);
            ctrl2.RenderTransform.BeginAnimation(TranslateTransform.XProperty, anim2x);
            ctrl2.RenderTransform.BeginAnimation(TranslateTransform.YProperty, anim2y);

        }


        public void DisplayString(string str)
        {
            if (str.Length == 0)
                Clear();

            if (str.Length > 6)
            {
                //lbl0.Content = "1";
                for (int i = labels.Count-1; i >=0 ; --i)
                {
                    (labels[i] as Label).Content = '?';
                }

            }
            else
            {
                Clear();
                for (int i = labels.Count - 1, j = 0; i >= labels.Count - str.Length; --i, ++j)
                {
                    Label lbl = labels[i] as Label;
                    if (lbl != null)
                        lbl.Content = str[str.Length - 1 - j];
                }
            }
        }

        public void Clear()
        {
            for (int i = 0; i < labels.Count; ++i)
            {
                Label lbl = labels[i] as Label;
                if (lbl != null)
                    lbl.Content = "";
            }
        }

        public event EventHandler<EventArgs> btnPressed;


        public int GetButtonIndex(object obj)
        {
            Button btn = obj as Button;
            if (btn == null)
                return -1;
            int num = buttons.IndexOf(btn);
            return num;
        }


        private bool state;

        public void SwapState()
        {
            if (state == false)
                state = true;
            else
                state = false;

            if (state == false)
            {
                foreach (ContentControl ctrl in buttons)
                    if (ctrl.Name != "btnOn")
                        ctrl.IsEnabled = false;
            }
            else
                foreach (ContentControl ctrl in buttons)
                    ctrl.IsEnabled = true;

        }

        public event EventHandler<EventArgs> onBtnPressed;

        private void btn_IsEnabledChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (state == false)
            {
                for (int i = 0; i < labels.Count; ++i)
                {
                    //((labels[i] as Label).Background as SolidColorBrush).Color = System.Windows.Media.Colors.Black;
                    
                }
            }
            else
            {
                for (int i = 0; i < labels.Count; ++i)
                {
                    ((labels[i] as Label).Background as SolidColorBrush).Color = System.Windows.Media.Colors.LightYellow;

                }
            }
        }
    }
}
