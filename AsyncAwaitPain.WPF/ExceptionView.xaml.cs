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

namespace AsyncAwaitPain.WPF
{
    /// <summary>
    /// Interaction logic for Exception.xaml
    /// </summary>
    public partial class ExceptionView : UserControl
    {
        public ExceptionView()
        {
            InitializeComponent();
        }

        public int Count
        {
            get { return (int)GetValue(DelayCountProperty); }
            set { SetValue(DelayCountProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Count.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DelayCountProperty =
            DependencyProperty.Register("Count", typeof(int), typeof(ExceptionView), new PropertyMetadata(0));



        private async Task Delay()
        {
            await Task.Delay(50);
            Count += 1;
            throw new System.Exception();
        }

        private void Abandon_Click(object sender, RoutedEventArgs e)
        {
            Delay();

            // Everything appears to complete because the exception is after the Count is updated
            // So where did the exception go?
            // TaskScheduler.UnobservedTaskException in App.xaml.cs
        }

        private async void AsyncVoid_Click(object sender, RoutedEventArgs e)
        {
            // Works!
            // This is the one allowable case to use Async Void
            await Delay();
        }

        private void AbandonTryCatch_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Delay();
            }
            catch (Exception ex)
            {
                // Exception is not caught
                MessageBox.Show($"Exception: {ex.Message}");
            }
        }

        private async void AsyncVoidTryCatch_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                await Delay();
            }
            catch (Exception ex)
            {
                // Exception is caught
                MessageBox.Show($"Exception: {ex.Message}");
            }
        }


    }
}
