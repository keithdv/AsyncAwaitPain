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
    public partial class AsyncConstructor : UserControl
    {
        public AsyncConstructor()
        {
            InitializeComponent();
        }









        public object oAsyncConstructor
        {
            get { return (object)GetValue(oAsyncConstructorProperty); }
            set { SetValue(oAsyncConstructorProperty, value); }
        }

        // Using a DependencyProperty as the backing store for oAsyncConstructor.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty oAsyncConstructorProperty =
            DependencyProperty.Register("oAsyncConstructor", typeof(object), typeof(AsyncConstructor), null);

        private void New_Click(object sender, RoutedEventArgs e)
        {
            oAsyncConstructor = new Lib.AsyncConstructor();

        }

        private void Exception_Click(object sender, RoutedEventArgs e)
        {
            oAsyncConstructor = new Lib.AsyncConstructorException();
        }

        private async void Initialize_Click(object sender, RoutedEventArgs e)
        {
            var o = new Lib.AsyncConstructorInitialize();

            oAsyncConstructor = o;

            await o.Initialize();

        }

        private async void InitializeException_Click(object sender, RoutedEventArgs e)
        {
            var o = new Lib.AsyncConstructorInitializeException();

            oAsyncConstructor = o;

            await o.Initialize();
        }
    }
}
