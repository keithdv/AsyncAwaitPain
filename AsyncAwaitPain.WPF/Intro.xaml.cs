using System;
using System.Collections.Generic;
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
    /// Interaction logic for Intro.xaml
    /// </summary>
    public partial class Intro : UserControl
    {
        public Intro()
        {
            InitializeComponent();
        }

        public int DelayCount
        {
            get { return (int)GetValue(DelayCountProperty); }
            set { SetValue(DelayCountProperty, value); }
        }

        // Using a DependencyProperty as the backing store for DelayCount.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DelayCountProperty =
            DependencyProperty.Register("DelayCount", typeof(int), typeof(Intro), new PropertyMetadata(0));


        public int MethodCount
        {
            get { return (int)GetValue(MethodCountProperty); }
            set { SetValue(MethodCountProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Count.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MethodCountProperty =
            DependencyProperty.Register("MethodCount", typeof(int), typeof(Intro), new PropertyMetadata(0));

        private async Task Delay()
        {
            await Task.Delay(TimeConstants._1second);
            DelayCount += 1;
        }


        private void Abandon_Click(object sender, RoutedEventArgs e)
        {
            Delay();
            // This happens before the DelayCount
            // Appears to succeed but not the desired behavior which leads to serious issues in other less simple cases
            MethodCount = MethodCount + 1;
        }

        private void Wait_Click(object sender, RoutedEventArgs e)
        {
            // Deadlocks every time

            //Delay().Wait();

            if (!Delay().Wait(TimeConstants._1second))
            {
                MessageBox.Show("Deadlock");
            }
            else
            {
                MethodCount = MethodCount + 1;
            }
        }

        // Acceptable case to use Async Void
        private async void AsyncVoid_Click(object sender, RoutedEventArgs e)
        {
            // Correct
            await Delay();
            MethodCount = MethodCount + 1;
        }

    }
}
