using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsyncAwaitPain.Lib
{
    public class BusinessObject
    {

        public BusinessObject()
        {
            Collection.CollectionChanged += Collection_CollectionChanged;
        }

        private async void Collection_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            await Task.Delay(1000);
            CompletedCount += 1;
        }

        private AsyncObservableCollection<string> Collection { get; set; } = new AsyncObservableCollection<string>();

        public void Operation()
        {
            Collection.Add("value");
        }

        private static object _lock = new object();

        public int CompletedCount { get; private set; } = 0;

    }
}
