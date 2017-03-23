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
    /// Interaction logic for AsyncEvent.xaml
    /// </summary>
    public partial class AsyncEvent : UserControl
    {
        public AsyncEvent()
        {
            InitializeComponent();
        }



        public string Successful
        {
            get { return (string)GetValue(SuccessfulProperty); }
            set { SetValue(SuccessfulProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Successful.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SuccessfulProperty =
            DependencyProperty.Register("Successful", typeof(string), typeof(AsyncEvent), new PropertyMetadata(null));



        private void Go_Click(object sender, RoutedEventArgs e)
        {
            var asyncEvent = new Lib.AsyncEvent();

            asyncEvent.Operation();

            Successful = asyncEvent.Completed ? "Yes" : "No";

        }
    }
}
