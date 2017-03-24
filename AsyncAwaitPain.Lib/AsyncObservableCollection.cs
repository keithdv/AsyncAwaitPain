using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AsyncAwaitPain.Lib
{
    public delegate Task NotifyCollectionChangedAsyncEventHandler(object sender, NotifyCollectionChangedEventArgs e);

    public class AsyncObservableCollection<T> : ObservableCollection<T>
    {

        public event NotifyCollectionChangedAsyncEventHandler CollectionChangedAsync;
        public AsyncObservableCollection() : base() { }

        public AsyncObservableCollection(IEnumerable<T> enumberable) : base(enumberable) { }

        public AsyncObservableCollection(List<T> list) : base(list) { }


        //public async Task AddAsync(T item)
        //{
        //    base.Add(item);
        //    await _collectionChangedTask;
        //}

        //private static object _lock = new object();

        //public async Task AddAsync(T item)
        //{
        //    lock (_lock)
        //    {
        //        base.Add(item);
        //        await _collectionChangedTask;
        //    }
        //}

        //private static object _lock = new object();

        //public async Task AddAsync(T item)
        //{
        //    Monitor.Enter(_lock);
        //    base.Add(item);
        //    await _collectionChangedTask;
        //    _collectionChangedTask = null;
        //    Monitor.Exit(_lock); // Put a break point here...it works! (That's not a good thing)
        //}

        // Ensure only one thread is adding or removing items
        private static AutoResetEvent are = new AutoResetEvent(true);

        public async Task AddAsync(T item)
        {
            are.WaitOne(); // Close gate - only allow one thread thru
            base.Add(item);
            await _collectionChangedTask;
            _collectionChangedTask = null;
            are.Set(); // Open gate - Gate can be opened by any thread
        }

        private Task _collectionChangedTask;

        protected override void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
        {
            base.OnCollectionChanged(e);
            _collectionChangedTask = CollectionChangedAsync?.Invoke(this, e);

        }
    }
}
