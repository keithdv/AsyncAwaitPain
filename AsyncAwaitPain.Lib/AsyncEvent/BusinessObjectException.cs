using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsyncAwaitPain.Lib.AsyncEvent
{
    public class BusinessObjectException : INotifyPropertyChanged
    {

        public BusinessObjectException()
        {
            Collection.CollectionChanged += Collection_CollectionChanged;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private async void Collection_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            await Task.Delay(1000);
            CompletedCount += 1;
            throw new Exception("Failure");
        }

        private ObservableCollection<string> Collection { get; set; } = new ObservableCollection<string>();

        public void Operation()
        {
            Collection.Add("value");
        }

        private int _CompletedCount;

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
