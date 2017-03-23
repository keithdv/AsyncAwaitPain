using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Navigation;

namespace AsyncAwaitPain.WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {

#if DEBUG
        // THIS IS NOT A CURE!!!
        private void TaskScheduler_UnobservedTaskException(object sender, UnobservedTaskExceptionEventArgs e)
        {
            //MessageBox.Show($"TaskScheduler.UnobservedTaskException event hit [{e.Exception.Flatten().ToString()}]");
        }
#endif

        private void Current_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            if(e.Exception is AggregateException)
            {
                var aggEx = (AggregateException)e.Exception;
                MessageBox.Show($"Dispatcher.UnhandledException event hit [{aggEx.Flatten().ToString()}]");
                e.Handled = true;
            }
            else if(e.Exception is Exception)
            {
                MessageBox.Show($"Dispatcher.UnhandledException event hit [{e.Exception?.Message}]");
                e.Handled = true;
            }

        }

        private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            MessageBox.Show($"AppDomain.UnhandledException event hit");
        }


        private void Application_Startup(object sender, StartupEventArgs e)
        {
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
            App.Current.DispatcherUnhandledException += Current_DispatcherUnhandledException;
            TaskScheduler.UnobservedTaskException += TaskScheduler_UnobservedTaskException;


        }
    }
}
