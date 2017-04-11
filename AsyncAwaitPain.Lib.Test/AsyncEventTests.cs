using AsyncAwaitPain.Lib.AsyncEvent;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AsyncAwaitPain.Lib.Test
{
    [TestClass]
    public class AsyncEventTests
    {

        [TestMethod]
        public void SyncOperation()
        {
            // Fails
            // The unit test finishes before the delay

            var bo = new BusinessObject();

            bo.Operation();
            bo.Operation();
            bo.Operation();

            Assert.AreEqual(3, bo.CompletedCount);

        }

        [TestMethod]
        public void SyncOperationException()
        {
            //Fails
            // The exception in the event handler is never caught

            var bo = new BusinessObjectException();

            Exception ex = null;

            try
            {
                bo.Operation();

            }
            catch (Exception ex_)
            {
                ex = ex_;
            }

            Assert.IsNotNull(ex);

        }

        [TestMethod]
        public async Task AsyncOperation()
        {
            var asyncEvent = new BusinessObjectAsync();

            await asyncEvent.OperationAsync();
            await asyncEvent.OperationAsync();
            await asyncEvent.OperationAsync();

            Assert.AreEqual(3, asyncEvent.CompletedCount);

        }

        [TestMethod]
        public async Task AsyncOperationException()
        {
            var bo = new BusinessObjectAsyncException();

            Exception ex = null;

            try
            {
                await bo.OperationAsync();

            }
            catch (Exception ex_)
            {
                ex = ex_;
            }

            Assert.IsNotNull(ex);

        }


        [TestMethod]
        public async Task BusinessObject_Simple_MultipleThreads()
        {

            var asyncEvent = new BusinessObjectAsync();

            var allTasks = new List<Task>();

            for (var i = 0; i < 20; i++)
            {

                List<Task> tasks = new List<Task>();

                for (var j = 0; j < 20; j++)
                {
                    tasks.Add(Task.Run(async () => await asyncEvent.OperationSimpleAsync()));
                }
                allTasks.AddRange(tasks);

                await Task.WhenAll(tasks);

            }


            Assert.AreEqual(400, asyncEvent.CompletedCount);

        }

        [TestMethod]
        public async Task BusinessObject_AutoResetEvent_MultipleThreads()
        {

            var asyncEvent = new BusinessObjectAsync();

            var allTasks = new List<Task>();

            var maxThreadCount = 0;

            var cancel = false;
            var task = Task.Run(async () =>
            {
                if (Process.GetCurrentProcess().Threads.Count > maxThreadCount)
                {
                    maxThreadCount = Process.GetCurrentProcess().Threads.Count;
                }
                await Task.Delay(50);
                if (cancel) { return; }
            });

            for (var i = 0; i < 20; i++)
            {

                List<Task> tasks = new List<Task>();

                for (var j = 0; j < 20; j++)
                {
                    tasks.Add(Task.Run(async () => await asyncEvent.OperationAsyncEx()));
                    await Task.Delay(15);
                }
                allTasks.AddRange(tasks);

                await Task.WhenAll(tasks);

            }


            Assert.AreEqual(400, asyncEvent.CompletedCount);

            cancel = true;
            await task;

        }

        private static object _lock = new object();

        [Ignore]
        [TestMethod]
        public async Task Monitor_Enter_Exit()
        {
            Monitor.Enter(_lock);
            await Task.Delay(100);
            Monitor.Exit(_lock);
        }

        [Ignore]
        [TestMethod]
        public async Task TaskCompletionSource()
        {
            var asyncEvent = new AsyncEventTaskCompletionSource();

            await asyncEvent.OperationAsync();

            Assert.IsTrue(asyncEvent.Completed);
        }

    }
}
