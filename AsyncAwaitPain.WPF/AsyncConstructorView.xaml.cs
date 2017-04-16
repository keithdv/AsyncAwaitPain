
using AsyncAwaitPain.Lib.Constructor;
using System.Windows;
using System.Windows.Controls;

namespace AsyncAwaitPain.WPF
{
    /// <summary>
    /// Interaction logic for ConfigureAwait.xaml
    /// </summary>
    public partial class AsyncConstructorView : UserControl
    {
        public AsyncConstructorView()
        {
            InitializeComponent();
        }



        public object Result
        {
            get { return (object)GetValue(ResultProperty); }
            set { SetValue(ResultProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Result.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ResultProperty =
            DependencyProperty.Register("Result", typeof(object), typeof(AsyncConstructorView), new PropertyMetadata(null));




        private void New_Click(object sender, RoutedEventArgs e)
        {
            Result = new AsyncConstructor();
        }

        private void Exception_Click(object sender, RoutedEventArgs e)
        {
            // Exception not caught
            Result = new AsyncConstructorException();
        }

        private async void Initialize_Click(object sender, RoutedEventArgs e)
        {
            var o = new AsyncConstructorInitialize();

            Result = o;

            await o.InitializeAsync();

        }

        private async void InitializeException_Click(object sender, RoutedEventArgs e)
        {
            var o = new AsyncConstructorInitializeException();

            Result = o;

            await o.InitializeAsync();
        }
    }
}
