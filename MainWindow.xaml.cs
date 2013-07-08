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
    public partial class MainWindow : Window
    {
        private Random generator;

        public MainWindow()
        {
            generator = new Random(10000000);
            
            InitializeComponent();
            buttons = canvasKeyboard.Children;
            foreach (ContentControl ctrl in buttons)
                ctrl.RenderTransform = new TranslateTransform();
        }

        private UIElementCollection buttons;

        private void btn1_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void btn_Click(object sender, RoutedEventArgs e)
        {
            MixButtons(0.35, 0);

            //SwapButtonsSequence(25, 5);
        }

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


        private void btn9_Click(object sender, RoutedEventArgs e)
        {
            int ind1 = generator.Next() % (buttons.Count );
            int ind2 = generator.Next() % (buttons.Count );
            SwapButtons(ind1, ind2, 2, 0.5);
        }

        private void SwapButtonsSequence(int count, double secDuration)
        {
            double secDurationOne = secDuration / count;
            Duration duration = new Duration(TimeSpan.FromSeconds(secDurationOne));
            for (int i = 0; i < count; ++i)
            {
                int ind1 = generator.Next() % (buttons.Count);
                int ind2 = generator.Next() % (buttons.Count);
                SwapButtons(ind1, ind2, secDurationOne, i * secDurationOne);
            }

        }


        private void SwapButtons(int ind1, int ind2, double secDuration, double secWait)
        {
            if (ind1 == ind2) return;

            ContentControl ctrl1 = buttons[ind1] as ContentControl;
            ContentControl ctrl2 = buttons[ind2] as ContentControl;

            if (ctrl1 == null || ctrl2 == null)
                return;

            double secHDuration = secDuration / 2;

            var top1 = Canvas.GetTop(ctrl1);// -(ctrl1.RenderTransform as TranslateTransform).Y;
            var left1 = Canvas.GetLeft(ctrl1);// +(ctrl1.RenderTransform as TranslateTransform).X;
            var top2 = Canvas.GetTop(ctrl2);// -(ctrl2.RenderTransform as TranslateTransform).Y;
            var left2 = Canvas.GetLeft(ctrl2);// +(ctrl2.RenderTransform as TranslateTransform).X;

            var offsetX1 = (ctrl1.RenderTransform as TranslateTransform).X;
            var offsetY1 = (ctrl1.RenderTransform as TranslateTransform).Y;
            var offsetX2 = (ctrl2.RenderTransform as TranslateTransform).X;
            var offsetY2 = (ctrl2.RenderTransform as TranslateTransform).Y;

            var acttop1 = top1 + offsetY1;
            var acttop2 = top2 + offsetY2;
            var actleft1 = left1 + offsetX1;
            var actleft2 = left2 + offsetX2;

            DoubleAnimation anim1x = new DoubleAnimation();
            //anim1x.From = (ctrl1.RenderTransform as TranslateTransform).X;
            anim1x.By = actleft2 - actleft1;
            //anim1x.To = offsetX1 + actleft2 - actleft1;
            anim1x.Duration = new Duration(TimeSpan.FromSeconds(secHDuration));
            anim1x.BeginTime = TimeSpan.FromSeconds(secWait);
            
            anim1x.AutoReverse = false;
            
            DoubleAnimation anim1y = new DoubleAnimation();
            //anim1y.From = (ctrl1.RenderTransform as TranslateTransform).Y;
            //anim1y.To = offsetY1 + acttop2 - acttop1;
            anim1y.By = acttop2 - acttop1;
            anim1y.Duration = new Duration(TimeSpan.FromSeconds(secDuration));
            anim1y.BeginTime = TimeSpan.FromSeconds(secWait);
            
            anim1y.AutoReverse = false;

            DoubleAnimation anim2x = new DoubleAnimation();
            //anim1x.From = (ctrl2.RenderTransform as TranslateTransform).X;
            //anim2x.To = offsetX2 + actleft1 - actleft2;
            anim2x.By = actleft1 - actleft2;
            anim2x.Duration = new Duration(TimeSpan.FromSeconds(secHDuration));
            anim2x.BeginTime = TimeSpan.FromSeconds(secWait);
            
            anim2x.AutoReverse = false;

            DoubleAnimation anim2y = new DoubleAnimation();
            //anim1y.From = (ctrl2.RenderTransform as TranslateTransform).Y;
            //anim2y.To = offsetY2 + acttop1 - acttop2;
            anim2y.By = acttop1 - acttop2;
            anim2y.Duration = new Duration(TimeSpan.FromSeconds(secDuration));
            anim2y.BeginTime = TimeSpan.FromSeconds(secWait);            
            anim2y.AutoReverse = false;

            if (actleft1 == actleft2)
            {
                anim1x.By = 30;
                anim1x.AutoReverse = true;
                anim1x.Duration = new Duration(TimeSpan.FromSeconds(secHDuration));

                anim2x.By = -30;
                anim2x.AutoReverse = true;
                anim2x.Duration = new Duration(TimeSpan.FromSeconds(secHDuration));
            }

            if (acttop1 == acttop2)
            {
                anim1y.By = 30;
                anim1y.AutoReverse = true;
                anim1y.Duration = new Duration(TimeSpan.FromSeconds(secHDuration));

                anim2y.By = -30;
                anim2y.AutoReverse = true;
                anim2y.Duration = new Duration(TimeSpan.FromSeconds(secHDuration));
            }

                       
            ctrl1.RenderTransform.BeginAnimation(TranslateTransform.XProperty, anim1x);
            ctrl1.RenderTransform.BeginAnimation(TranslateTransform.YProperty, anim1y);
            ctrl2.RenderTransform.BeginAnimation(TranslateTransform.XProperty, anim2x);
            ctrl2.RenderTransform.BeginAnimation(TranslateTransform.YProperty, anim2y);

                        
            Canvas.SetTop(ctrl1, Canvas.GetTop(ctrl1));// - (ctrl1.RenderTransform as TranslateTransform).Y);
            Canvas.SetLeft(ctrl1, Canvas.GetLeft(ctrl1));// + (ctrl1.RenderTransform as TranslateTransform).X);
            Canvas.SetTop(ctrl2, Canvas.GetTop(ctrl2));// - (ctrl2.RenderTransform as TranslateTransform).Y);
            Canvas.SetLeft(ctrl2, Canvas.GetLeft(ctrl2));// + (ctrl2.RenderTransform as TranslateTransform).X);


        }

    }
}
