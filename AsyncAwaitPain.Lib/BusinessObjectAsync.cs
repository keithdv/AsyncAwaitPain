using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsyncAwaitPain.Lib
{
    public class BusinessObjectAsync
    {

        public BusinessObjectAsync()
        {
            Collection = new AsyncObservableCollection<string>();
            Collection.CollectionChangedAsync += Collection_CollectionChangedAsync;
        }

        private async Task Collection_CollectionChangedAsync(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            await Task.Delay(1000);
            CompletedCount += 1;
        }

        private AsyncObservableCollection<string> Collection { get; set; }

        public async Task OperationAsyc()
        {
            await Collection.AddAsync("value");
        }

        private static object _lock = new object();

        public int CompletedCount { get; private set; } = 0;

    }
}
