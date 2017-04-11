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


        public int ClickCount
        {
            get { return (int)GetValue(ClickCountProperty); }
            set { SetValue(ClickCountProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Count.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ClickCountProperty =
            DependencyProperty.Register("ClickCount", typeof(int), typeof(ConfigureAwait), new PropertyMetadata(0));


        private async Task DelayConfigureAwaitFalse()
        {
            await Task.Delay(50).ConfigureAwait(false);
            ClickCount += 1;
        }


        private void Wait_Click(object sender, RoutedEventArgs e)
        {
            // This will cause an error when DelayCount is used
            // because this is bound to 
            // which will try to access the control
            // but not on the UI context
            if (!DelayConfigureAwaitFalse().Wait(TimeConstants._5seconds))
            {
                MessageBox.Show("Deadlock!");
            }
        }

        private async Task DelayConfigureAwaitFalseNestedA()
        {
            await DelayConfigureAwaitFalseNestedB(); // We're making the decision to continue on the UI context
            ClickCount += 1;
        }

        private async Task DelayConfigureAwaitFalseNestedB()
        {
            await Task.Delay(TimeConstants._1second).ConfigureAwait(false);
            // ConfigureAwait(False) so we're in a new context
            // Doesn't matter we're not updated any UI controls here

            // This allows shared (ASP, Xaml) libraries can have ConfigureAwait(false);
        }

        private async void NestedConfigureAwait_Click(object sender, RoutedEventArgs e)
        {
            await DelayConfigureAwaitFalseNestedA();
        }
    }
}
