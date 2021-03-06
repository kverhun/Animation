﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Shapes;

namespace Animation
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            //base.OnStartup(e);


            ////this.MainWindow = new MainWindow();
            //MainWindow window = new MainWindow();
            //IView view = (MainWindow)this.MainWindow;
            //ViewModel vm = new ViewModel(view);
            ////window.DataContext = vm;
            ////window.Show();
        }


        public void btnMouseEnter(object sender, MouseEventArgs e)
        {
            Button btn = sender as Button;
            if (btn == null)
                return;

            Grid grid = btn.Template.FindName("grid", btn) as Grid;
            if (grid == null)
                return;
            Ellipse ellipse = grid.FindName("ellipse") as Ellipse;
            if (ellipse == null)
                return;
        }

    }
}
