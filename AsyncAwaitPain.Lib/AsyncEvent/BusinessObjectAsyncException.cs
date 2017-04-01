using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsyncAwaitPain.Lib.AsyncEvent
{
    public class BusinessObjectAsyncException : INotifyPropertyChanged
    {

        public BusinessObjectAsyncException()
        {
            Collection = new AsyncObservableCollection<string>();
            Collection.CollectionChangedAsync += Collection_CollectionChangedAsync;
        }

        private async Task Collection_CollectionChangedAsync(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            await Task.Delay(1000);
            CompletedCount += 1;
            throw new Exception("Failure");
        }

        private AsyncObservableCollection<string> Collection { get; set; }

        public async Task OperationAsync()
        {
            await Collection.AddAsync("value");
        }

        private int _CompletedCount;

        public event PropertyChangedEventHandler PropertyChanged;

        public int CompletedCount
        {
            get { return _CompletedCount; }
            set
            {
                _CompletedCount = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CompletedCount)));
            }
        }

    }
}
