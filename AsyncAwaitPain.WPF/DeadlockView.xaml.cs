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
    /// Interaction logic for ConfigureAwait.xaml
    /// </summary>
    public partial class DeadlockView : UserControl
    {
        public DeadlockView()
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
            DependencyProperty.Register("ClickCount", typeof(int), typeof(DeadlockView), new PropertyMetadata(0));

        private async Task Delay()
        {
            await Task.Delay(TimeConstants._500milliseconds);
        }

        private void Deadlock_Click(object sender, RoutedEventArgs e)
        {
            // Deadlocks every time
            // I'm not yielding I'm blocking
            // No await - no ExecutionContext management

            if (!Delay().Wait(TimeConstants._1second))
            {
                MessageBox.Show("Deadlock");
            }
        }

        private async Task DelayConfigureAwaitFalse()
        {
            // ConfigureAwait(false) the answer? 
            // Not when there's a UI thread involved!
            await Task.Delay(50).ConfigureAwait(false);
            ClickCount += 1;
        }

        private void Wait_Click(object sender, RoutedEventArgs e)
        {
            // This will cause an error when ClickCount is used
            // because this is data bound to 
            // which will try to access the control
            // but not on the UI context
            if (!DelayConfigureAwaitFalse().Wait(TimeConstants._5seconds))
            {
                MessageBox.Show("Deadlock!");
            }
        }

        private async void AsyncVoid_Click(object sender, RoutedEventArgs e)
        {
            // Same issue as Wait_Click
            await DelayConfigureAwaitFalse();
        }


        private async Task DelayConfigureAwaitFalseInLibrary()
        {
            await Task.Delay(TimeConstants._1second).ConfigureAwait(false);
            // ConfigureAwait(False) so we're in a new context
            // Doesn't matter we're not updated any UI controls here

        }

        private async void LibraryConfigureAwait_Click(object sender, RoutedEventArgs e)
        {
            await DelayConfigureAwaitFalseInLibrary();

            // Back in the UI Context since we didn't define ConfigureAwait(false)
            // This allows shared business or framework libraries can have ConfigureAwait(false);

            ClickCount += 1;
        }

        public static object _TaskCompleted = new object();
        public static AutoResetEvent autoResetEvent = new AutoResetEvent(false);

        private void TaskMethod(Action callBack)
        {
            // do some work

            // Run on the UI thread..but wait the UI thread is blocked waiting for me!
            Dispatcher.Invoke(callBack);

            // Notify that I'm completed
            autoResetEvent.Set();
        }

        private void CallBack()
        {
            ClickCount++;
        }

        private void Example_Click(object sender, RoutedEventArgs e)
        {

            Task.Run(() => TaskMethod(CallBack));

            autoResetEvent.WaitOne(); // Block the UI thread
        }
    }
}
