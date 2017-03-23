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

        public void _threadCountTimer_CallBack(object state)
        {
            var context = (SynchronizationContext)state;

            context.Post((o) =>
                            {
                                ThreadCount = Process.GetCurrentProcess().Threads.Count;
                            }, null);

        }


        public int DelayCount
        {
            get { return (int)GetValue(DelayCountProperty); }
            set { SetValue(DelayCountProperty, value); }
        }

        // Using a DependencyProperty as the backing store for DelayCount.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DelayCountProperty =
            DependencyProperty.Register("DelayCount", typeof(int), typeof(MainWindow), new PropertyMetadata(0));


        public int MethodCount
        {
            get { return (int)GetValue(MethodCountProperty); }
            set { SetValue(MethodCountProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Count.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MethodCountProperty =
            DependencyProperty.Register("MethodCount", typeof(int), typeof(MainWindow), new PropertyMetadata(0));



        public int ThreadCount
        {
            get { return (int)GetValue(ThreadCountProperty); }
            set { SetValue(ThreadCountProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ThreadCount.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ThreadCountProperty =
            DependencyProperty.Register("ThreadCount", typeof(int), typeof(MainWindow), new PropertyMetadata(0));



        private async Task Delay()
        {
            await Task.Delay(50);
            DelayCount += 1;
        }

        private void Abandon_Click(object sender, RoutedEventArgs e)
        {
            Delay();
            MethodCount = MethodCount + 1;
        }
        private void Wait_Click(object sender, RoutedEventArgs e)
        {
            if (!Delay().Wait(500))
            {
                MessageBox.Show("Blocked");
            }
        }


        private void Collect_Click(object sender, RoutedEventArgs e)
        {
            GC.Collect();
        }
    }
}
