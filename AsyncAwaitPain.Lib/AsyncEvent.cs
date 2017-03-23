using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsyncAwaitPain.Lib
{
    public class AsyncEvent
    {

        public AsyncEvent()
        {
            Collection.CollectionChanged += Collection_CollectionChanged;
        }

        private async void Collection_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            await Task.Delay(5);
        }

        private ObservableCollection<string> Collection { get; set; } = new ObservableCollection<string>();

        public void Operation()
        {
            Collection.Add("value");
        }

        public bool Completed { get; private set; } = false;

    }
}
