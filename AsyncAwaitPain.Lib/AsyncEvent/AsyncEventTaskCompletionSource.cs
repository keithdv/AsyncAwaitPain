using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsyncAwaitPain.Lib.AsyncEvent
{
    public class AsyncEventTaskCompletionSource
    {


        public AsyncEventTaskCompletionSource()
        {
            Collection.CollectionChanged += Collection_CollectionChanged;
        }

        TaskCompletionSource<object> tcs;

        private async void Collection_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            await Task.Delay(1000);
            Completed = true;
            tcs.SetResult(null);
        }

        private ObservableCollectionAsync<string> Collection { get; set; } = new ObservableCollectionAsync<string>();

        public async Task OperationAsync()
        {
            tcs = new TaskCompletionSource<object>();
            Collection.Add("value");
            await tcs.Task;
            
        }

        public bool Completed { get; private set; } = false;


    }
}
