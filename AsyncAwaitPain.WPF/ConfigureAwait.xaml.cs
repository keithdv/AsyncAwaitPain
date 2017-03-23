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
    /// Interaction logic for ConfigureAwait.xaml
    /// </summary>
    public partial class ConfigureAwait : UserControl
    {
        public ConfigureAwait()
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
            DependencyProperty.Register("DelayCount", typeof(int), typeof(ConfigureAwait), new PropertyMetadata(0));


        public int MethodCount
        {
            get { return (int)GetValue(MethodCountProperty); }
            set { SetValue(MethodCountProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Count.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MethodCountProperty =
            DependencyProperty.Register("MethodCount", typeof(int), typeof(ConfigureAwait), new PropertyMetadata(0));


        private async Task Delay()
        {
            await Task.Delay(50).ConfigureAwait(false);
            DelayCount += 1;
        }

        private void ConfigureAwaitFalse_Click(object sender, RoutedEventArgs e)
        {
            Delay();
            MethodCount += 1;
        }

        private void Wait_Click(object sender, RoutedEventArgs e)
        {
            Delay().Wait();
            MethodCount += 1;
        }
    }
}
