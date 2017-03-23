using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsyncAwaitPain.Lib
{
    public class AsyncEventTaskQueue
    {

        public AsyncEventTaskQueue()
        {
            Collection.CollectionChanged += Collection_CollectionChanged;
        }

        private Queue<Task> collectionChangedTask = new Queue<Task>();

        private void Collection_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            collectionChangedTask.Enqueue(DelayAsync());
            Completed++;
        }

        private async Task DelayAsync()
        {
            await Task.Delay(1000);
        }

        private ObservableCollection<string> Collection { get; set; } = new ObservableCollection<string>();

        public async Task OperationAsyc()
        {

            Collection.Add("value1");
            Collection.Add("value2");
            Collection.Add("value3");

            while(collectionChangedTask.Count > 0)
            {
                await collectionChangedTask.Dequeue();
            }

        }

        public int Completed { get; private set; } = 0;

    }
}
