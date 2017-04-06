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

        [Ignore]
        [TestMethod]
        public async Task BusinessObject_Simple_MultipleThreads()
        {
            // Sometimes fails...sometimes doesn't
            // Thought this fails because of the single instance of the task
            // Now I'm not so sure...

            var asyncEvent = new BusinessObjectAsync();

            List<Task> tasks = new List<Task>();

            tasks.Add(Task.Run(() => asyncEvent.OperationSimpleAsync()));
            tasks.Add(Task.Run(() => asyncEvent.OperationSimpleAsync()));
            tasks.Add(Task.Run(() => asyncEvent.OperationSimpleAsync()));

            await Task.WhenAll(tasks);

            Assert.AreEqual(3, asyncEvent.CompletedCount);

        }

        [TestMethod]
        public async Task BusinessObject_AutoResetEvent_MultipleThreads()
        {

            var asyncEvent = new BusinessObjectAsync();

            List<Task> tasks = new List<Task>();

            tasks.Add(Task.Run(() => asyncEvent.OperationAsync()));
            tasks.Add(Task.Run(() => asyncEvent.OperationAsync()));
            tasks.Add(Task.Run(() => asyncEvent.OperationAsync()));

            await Task.WhenAll(tasks);

            Assert.AreEqual(3, asyncEvent.CompletedCount);

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
