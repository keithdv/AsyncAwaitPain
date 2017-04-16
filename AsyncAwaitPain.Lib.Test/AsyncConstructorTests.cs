using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using AsyncAwaitPain.Lib.Constructor;

namespace AsyncAwaitPain.Lib.Test
{
    [TestClass]
    public class AsyncConstructorTests
    {
        [TestMethod]
        public void New()
        {
            var o = new AsyncConstructor();

            Assert.IsTrue(o.Completed);

        }

        [TestMethod]
        public void Exception()
        {
            var o = new AsyncConstructorException();
        }

        [TestMethod]
        public async Task YieldException()
        {
            // The unit test completes
            // but the exception is lost!
            var o = new AsyncConstructorException();


            for (var i = 0; i < 100 && o.Completed == false; i++) // Infinite loop
            {
                await Task.Yield();
                await Task.Delay(10);
            }

            // Not completed and no exception raised
            // Exception is lost
            Assert.IsTrue(o.Completed);

        }


        [TestMethod]
        public async Task Initialize()
        {
            var o = new AsyncConstructorInitialize();

            await o.InitializeAsync();

            Assert.IsTrue(o.Completed);

        }

        [TestMethod]
        public async Task InitializeException()
        {
            var o = new AsyncConstructorInitializeException();

            await o.InitializeAsync();
            
        }

    }
}
