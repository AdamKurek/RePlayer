using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace RePlayer
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {

            if (e.Args.Length == 1)
            {
                var window = new MainWindow(e.Args[0]);
                window.Show();
            }
            else
            {
                var window = new MainWindow();
                window.Show();
            }
        }


        //public App()
        //{
        //    if (Environment.GetCommandLineArgs().Length == 1)
        //    {
        //        var window = new MainWindow(Environment.GetCommandLineArgs()[0]);
        //        window.Show();
        //    }
        //    else
        //    {
        //        var window = new MainWindow();
        //        window.Show();
        //    }
        //}

    }

}
