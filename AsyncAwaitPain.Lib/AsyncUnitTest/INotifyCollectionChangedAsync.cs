using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsyncAwaitPain.Lib.AsyncUnitTest
{
    public delegate Task NotifyCollectionChangedAsyncEventHandler(object sender, NotifyCollectionChangedEventArgs e);

    public interface INotifyCollectionChangedAsync
    {
        //
        // Summary:
        //     Occurs when the collection changes.
        event NotifyCollectionChangedAsyncEventHandler CollectionChangedAsync;

    }
}
