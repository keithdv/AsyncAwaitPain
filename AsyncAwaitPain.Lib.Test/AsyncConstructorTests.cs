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

            await o.Initialize();
            
        }

    }
}
