using AsyncAwaitPain.Lib.AsyncEvent;
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
    public partial class AsyncEventView : UserControl
    {
        public AsyncEventView()
        {
            InitializeComponent();
        }




        public BusinessObject BusinessObject
        {
            get { return (BusinessObject)GetValue(BusinessObjectProperty); }
            set { SetValue(BusinessObjectProperty, value); }
        }

        // Using a DependencyProperty as the backing store for BusinessObject.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty BusinessObjectProperty =
            DependencyProperty.Register("BusinessObject", typeof(BusinessObject), typeof(AsyncEventView), new PropertyMetadata(new BusinessObject()));

        public BusinessObjectAsync BusinessObjectAsync
        {
            get { return (BusinessObjectAsync)GetValue(BusinessObjectAsyncProperty); }
            set { SetValue(BusinessObjectAsyncProperty, value); }
        }

        // Using a DependencyProperty as the backing store for BusinessObjectAsync.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty BusinessObjectAsyncProperty =
            DependencyProperty.Register("BusinessObjectAsync", typeof(BusinessObjectAsync), typeof(AsyncEventView), new PropertyMetadata(new BusinessObjectAsync()));



        public BusinessObjectException BusinessObjectException
        {
            get { return (BusinessObjectException)GetValue(BusinessObjectExceptionProperty); }
            set { SetValue(BusinessObjectExceptionProperty, value); }
        }

        // Using a DependencyProperty as the backing store for BusinessObjectException.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty BusinessObjectExceptionProperty =
            DependencyProperty.Register("BusinessObjectException", typeof(BusinessObjectException), typeof(AsyncEventView), new PropertyMetadata(new BusinessObjectException()));



        private void SyncOperation_Click(object sender, RoutedEventArgs e)
        {

            BusinessObject.Operation();
        }

        private async void AsyncOperation_Click(object sender, RoutedEventArgs e)
        {
            await BusinessObjectAsync.OperationAsync();
        }

        private void SyncOperationException_Click(object sender, RoutedEventArgs e)
        {
            // What appears to work in WPF doesn't work in WebAPI (Request/Response) or unit testing

            BusinessObjectException.Operation();
        }
    }
}
