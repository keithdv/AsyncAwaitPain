using AsyncAwaitPain.Lib.AsyncEvent;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AsyncAwaitPain.Lib.Test
{

    [TestClass]
    public class AsyncObservableCollectionTests
    {


        [TestMethod]
        public async Task SimpleAdd()
        {
            var completedCount = 0;
            var collection = new AsyncObservableCollection<string>();

            collection.CollectionChangedAsync += async (o, e) =>
            {
                await Task.Delay(1000);
                completedCount++;
            };

            await collection.AddAsync("value");
            Assert.AreEqual(1, completedCount);
        }


        [TestMethod]
        public async Task SimpleAdd_MultipleThreads()
        {
            var completedCount = 0;
            var collection = new AsyncObservableCollection<string>();

            collection.CollectionChangedAsync += async (o, e) =>
            {
                await Task.Delay(1000);
                completedCount++;
            };

            // This should fail like AsyncEventTests.BusinessObject_Simple_MultipleThreads!!!
            List<Task> tasks = new List<Task>();

            tasks.Add(Task.Run(() => collection.SimpleAddAsync("value")));
            tasks.Add(Task.Run(() => collection.SimpleAddAsync("value")));
            tasks.Add(Task.Run(() => collection.SimpleAddAsync("value")));

            await Task.WhenAll(tasks);

            Assert.AreEqual(3, completedCount);

        }

        [TestMethod]
        public async Task MonitorAdd_MultipleThreads()
        {
            var completedCount = 0;
            var collection = new AsyncObservableCollection<string>();

            collection.CollectionChangedAsync += async (o, e) =>
            {
                await Task.Delay(1000);
                completedCount++;
            };

            // Deadlock
            List<Task> tasks = new List<Task>();

            tasks.Add(Task.Run(() => collection.MonitorAddAsync("value")));
            tasks.Add(Task.Run(() => collection.MonitorAddAsync("value")));
            tasks.Add(Task.Run(() => collection.MonitorAddAsync("value")));

            await Task.WhenAll(tasks);

            Assert.AreEqual(3, completedCount);

        }

        private static object _lock = new object();

        [TestMethod]
        public async Task Monitor_Enter_Exit()
        {
            // Exception: System.Threading.SynchronizationLockException: Object synchronization method was called from an unsynchronized block of code.

            Monitor.Enter(_lock);
            await Task.Delay(100);
            Monitor.Exit(_lock);
        }


        [TestMethod]
        public async Task AutoResetEvent_MultipleThreads()
        {
            var completedCount = 0;
            var collection = new AsyncObservableCollection<string>();

            collection.CollectionChangedAsync += async (o, e) =>
            {
                await Task.Delay(1000);
                completedCount++;
            };

            List<Task> tasks = new List<Task>();

            tasks.Add(Task.Run(() => collection.AddAsync("value")));
            tasks.Add(Task.Run(() => collection.AddAsync("value")));
            tasks.Add(Task.Run(() => collection.AddAsync("value")));

            await Task.WhenAll(tasks);

            Assert.AreEqual(3, completedCount);

        }



        [TestMethod]
        public async Task TaskCompletionSource()
        {
            var asyncEvent = new AsyncEventTaskCompletionSource();

            await asyncEvent.OperationAsync();

            Assert.IsTrue(asyncEvent.Completed);
        }


    }
}
