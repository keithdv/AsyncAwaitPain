using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsyncAwaitPain.Lib.AsyncEvent
{
    public interface IObservableCollectionAsync<T> : INotifyCollectionChangedAsync, IList<T>, ICollection<T>, IEnumerable<T>, IEnumerable, IList, ICollection, IReadOnlyList<T>, IReadOnlyCollection<T>, INotifyCollectionChanged, INotifyPropertyChanged
    {
        /// <summary>
        /// Async and thread safe using AutoResetEvent
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        Task AddAsync_ThreadSafe(T item);

        Task AddAsync(T item);
    }
}
