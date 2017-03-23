using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsyncAwaitPain.Lib.Test
{
    [TestClass]
    public class AsyncEventTests
    {

        [TestMethod]
        public void Operation()
        {
            var asyncEvent = new AsyncEvent();

            asyncEvent.Operation();

            //Assert.IsTrue(asyncEvent.Completed);

        }


        [TestMethod]
        public async Task TaskQueue()
        {
            var asyncEvent = new AsyncEventTaskQueue();

            await asyncEvent.OperationAsyc();

            Assert.AreEqual(3, asyncEvent.Completed);

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
