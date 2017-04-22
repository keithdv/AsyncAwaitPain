using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsyncAwaitPain.Lib.AsyncEvent
{
    public class BusinessObject : INotifyPropertyChanged
    {

        public BusinessObject()
        {
            Collection.CollectionChanged += Collection_CollectionChanged;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        // Async Void does not work in WebApi or WPF
        private async void Collection_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            await Task.Delay(1000);
            CompletedCount += 1;
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
