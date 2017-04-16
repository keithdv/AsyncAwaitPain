using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AsyncAwaitPain.Lib.AsyncEvent
{

    public class ObservableCollectionAsync<T> : ObservableCollection<T>, IObservableCollectionAsync<T>
    {

        public event NotifyCollectionChangedAsyncEventHandler CollectionChangedAsync;
        public ObservableCollectionAsync() : base() { }

        public ObservableCollectionAsync(IEnumerable<T> enumberable) : base(enumberable) { }

        public ObservableCollectionAsync(List<T> list) : base(list) { }

        private Task _collectionChangedTask;

        protected override void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
        {
            base.OnCollectionChanged(e);
            _collectionChangedTask = CollectionChangedAsync?.Invoke(this, e);

        }

        public Task SimpleAddAsync(T item)
        {
            base.Add(item);
            return _collectionChangedTask;
        }

        /// Does not compile
        /// You cannot have an async within a Lock
        /// Lock must be release by the owner thread 
        /// Not guarenteed with Async
        //public async Task LockAddAsync(T item)
        //{
        //    lock (_lock)
        //    {
        //        base.Add(item);
        //        await _collectionChangedTask;
        //    }
        //}

        private static object _lock = new object();

        // So we bypass lock
        // And use the underlying type - Monitor
        // Deadlock or Exception
        public async Task MonitorAddAsync(T item)
        {
            Monitor.Enter(_lock);
            base.Add(item);
            await _collectionChangedTask;
            _collectionChangedTask = null;
            Monitor.Exit(_lock); // Put a break point here...it works! (That's not a good thing)
        }

        // Ensure only one thread is adding or removing items
        private static AutoResetEvent are = new AutoResetEvent(true);

        public Task AddAsync(T item)
        {

            // Since there is only one instance of _collectionChangeTask
            // available we have to be careful not to overwrite it during multithreaded operations
            // But you cannot use Monitor.Enter and Monitor.Exit within an asyncrnous block of code

            are.WaitOne(); // Close gate - only allow one thread thru

            Task t = null;

            try
            {
                base.Add(item);
                t = _collectionChangedTask;
            }
            catch (Exception)
            {
                are.Set(); // Open gate - Gate can be opened by any thread
                throw;
            }

            are.Set(); // Open gate - Gate can be opened by any thread
            return t;

        }
        

    }
}
