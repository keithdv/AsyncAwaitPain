using AsyncAwaitPain.Lib.Constructor;
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
    public partial class AsyncConstructorView : UserControl
    {
        public AsyncConstructorView()
        {
            InitializeComponent();
        }

        public AsyncConstructor AsyncConstructor
        {
            get { return (AsyncConstructor)GetValue(AsyncConstructorProperty); }
            set { SetValue(AsyncConstructorProperty, value); }
        }

        public static readonly DependencyProperty AsyncConstructorProperty =
            DependencyProperty.Register("AsyncConstructor", typeof(AsyncConstructor), typeof(AsyncConstructor), null);


        private void New_Click(object sender, RoutedEventArgs e)
        {
            AsyncConstructor = new AsyncConstructor();
        }

        private void Exception_Click(object sender, RoutedEventArgs e)
        {
            //AsyncConstructor = new AsyncConstructorException();
        }

        private async void Initialize_Click(object sender, RoutedEventArgs e)
        {
            //var o = new AsyncConstructorInitialize();

            //AsyncConstructor = o;

            //await o.InitializeAsync();

        }

        private async void InitializeException_Click(object sender, RoutedEventArgs e)
        {
            var o = new AsyncConstructorInitializeException();

            //AsyncConstructor = o;

            //await o.Initialize();
        }
    }
}
