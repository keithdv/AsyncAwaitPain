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
        public void Operation()
        {
            var asyncEvent = new BusinessObject();

            asyncEvent.Operation();
            asyncEvent.Operation();
            asyncEvent.Operation();

            Assert.AreEqual(3, asyncEvent.CompletedCount);

        }


        [TestMethod]
        public async Task BusinessObject()
        {
            var asyncEvent = new BusinessObjectAsync();

            await asyncEvent.OperationAsyc();
            await asyncEvent.OperationAsyc();
            await asyncEvent.OperationAsyc();

            Assert.AreEqual(3, asyncEvent.CompletedCount);

        }



        [TestMethod]
        public async Task BusinessObject_MultipleThreads()
        {
            var asyncEvent = new BusinessObjectAsync();

            List<Task> tasks = new List<Task>();

            tasks.Add(Task.Run(() => asyncEvent.OperationAsyc()));
            tasks.Add(Task.Run(() => asyncEvent.OperationAsyc()));
            tasks.Add(Task.Run(() => asyncEvent.OperationAsyc()));

            await Task.WhenAll(tasks);

            Assert.AreEqual(3, asyncEvent.CompletedCount);

        }

        private static object _lock = new object();

        [TestMethod]
        public async Task Monitor_Enter_Exit()
        {
            Monitor.Enter(_lock);
            await Task.Delay(100);
            Monitor.Exit(_lock);
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
