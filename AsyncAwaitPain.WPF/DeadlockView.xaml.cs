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

            // Since there isn't a ConfigureAwait(false)
            // this is in essence
            //Dispatcher.BeginInvoke(() =>
            //{

            // This is also considered part of the Task
            // so the Task isn't completed until this is completed
            ClickCount += 1;

            //});
        }

        private void Deadlock_Click(object sender, RoutedEventArgs e)
        {
            // Deadlocks every time
            // Tasks attempts to post "ClickCount += 1" on the UI thread using SynchronizationContext

            if (!Delay().Wait(TimeConstants._5seconds))
            {
                MessageBox.Show("Deadlock");
            }
        }

        private AutoResetEvent taskWait = new AutoResetEvent(false);

        private void TaskMethod(object data)
        {
            // do some work
            Thread.Sleep(TimeConstants._500milliseconds);

            // Run on the UI thread - this is what DispatcherSynchronizationContext does for us
            // but wait the UI thread is blocked waiting for me (the "Task") to complete!
            Dispatcher.Invoke(() =>
            {
                ClickCount++;
            });

            // Signal "Task" is completed
            taskWait.Set();
        }

        private void ThreadDeadlock_Click(object sender, RoutedEventArgs e)
        {
            Thread taskThread = new Thread(TaskMethod);

            taskThread.Start();

            if (!taskWait.WaitOne(TimeConstants._5seconds))
            {
                MessageBox.Show("Deadlock");
            }
        }

        private async Task DelayConfigureAwaitFalseFail()
        {
            // ConfigureAwait(false) the answer? Possibly
            await Task.Delay(TimeConstants._500milliseconds).ConfigureAwait(false);

            // Fails - We need this to run on the UI thread using Dispatcher.Invoke
            ClickCount += 1;

        }

        private void WaitFail_Click(object sender, RoutedEventArgs e)
        {
            // This will cause an error when ClickCount is used
            // because this is data bound to 
            // which will try to access the control
            // but not on the UI context
            if (!DelayConfigureAwaitFalseFail().Wait(TimeConstants._5seconds))
            {
                MessageBox.Show("Deadlock!");
            }
        }

        private void TaskMethodConfigureAwaitFalseFail(object data)
        {
            // do some work
            Thread.Sleep(TimeConstants._500milliseconds);

            // Simulated ConfigureAwait(false) - Let execution continue on the thread
            // Exception: This is not the UI thread
            ClickCount++;

            // Signal TaskMethod is completed
            taskWait.Set();
        }

        private void ThreadFail_Click(object sender, RoutedEventArgs e)
        {
            Thread taskThread = new Thread(TaskMethodConfigureAwaitFalseFail);

            taskThread.Start();

            if (!taskWait.WaitOne(TimeConstants._5seconds))
            {
                MessageBox.Show("Deadlock");
            }

        }

        private async Task DelayConfigureAwaitFalseSucceed()
        {
            // ConfigureAwait(false) the answer? Possibly
            await Task.Delay(TimeConstants._500milliseconds).ConfigureAwait(false);

            // As long as the library doesn't update any UI controls using DataBinding
            // this is ok! So use ConfigureAwait(false) in your libraries
            // That's what Microsoft meant for you to do 
            // The zen of async: Best practices for best performance - Alex Turner
            // https://www.youtube.com/watch?v=vu2kEstfuc8
        }

        private void WaitSucceed_Click(object sender, RoutedEventArgs e)
        {
            // This will cause an error when ClickCount is used
            // because this is data bound to 
            // which will try to access the control
            // but not on the UI context
            if (!DelayConfigureAwaitFalseSucceed().Wait(TimeConstants._5seconds))
            {
                MessageBox.Show("Deadlock!");
            }
            // The DelayConfigureAwaitFalseSucceed method has completed
            // so the task has completed
            // Back in the UI Context
            // Succeeds
            ClickCount += 1;

        }

        private void TaskMethodConfigureAwaitFalseSucceed(object data)
        {
            // do some work
            Thread.Sleep(TimeConstants._500milliseconds);

            // Let execution continue on the thread (no more work)

            // Signal TaskMethod is completed
            taskWait.Set();
        }

        private void ThreadSucceed_Click(object sender, RoutedEventArgs e)
        {
            Thread taskThread = new Thread(TaskMethodConfigureAwaitFalseSucceed);

            taskThread.Start();

            if (!taskWait.WaitOne(TimeConstants._5seconds))
            {
                MessageBox.Show("Deadlock");
            }

            // Back on the UI thread
            ClickCount++;

        }



    }
}
