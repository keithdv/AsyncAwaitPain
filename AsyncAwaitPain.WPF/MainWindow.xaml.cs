using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
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

namespace AsyncAwaitPain.WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private Timer _threadCountTimer;

        public MainWindow()
        {
            InitializeComponent();

            _threadCountTimer = new Timer(_threadCountTimer_CallBack, SynchronizationContext.Current, 250, 250);

        }


        public int ThreadCount
        {
            get { return (int)GetValue(ThreadCountProperty); }
            set { SetValue(ThreadCountProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ThreadCount.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ThreadCountProperty =
            DependencyProperty.Register("ThreadCount", typeof(int), typeof(Intro), new PropertyMetadata(0));



        public void _threadCountTimer_CallBack(object state)
        {
            var context = (SynchronizationContext)state;

            context.Post((o) =>
                            {
                                ThreadCount = Process.GetCurrentProcess().Threads.Count;
                            }, null);

        }




        private void Collect_Click(object sender, RoutedEventArgs e)
        {
            GC.Collect();
        }


        public static bool ShowUnhandledTaskExceptionMessageBox { get; set; }
        private void TaskEventEnabled_Checked(object sender, RoutedEventArgs e)
        {
            ShowUnhandledTaskExceptionMessageBox = true;
            this.TaskEventDisable.IsChecked = false;
        }

        private void TaskEventDisable_Checked(object sender, RoutedEventArgs e)
        {
            ShowUnhandledTaskExceptionMessageBox = false;
            this.TaskEventEnabled.IsChecked = false;
        }


    }
}
