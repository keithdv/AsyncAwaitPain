using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsyncAwaitPain.Lib.Test
{
    [TestClass]
    public class ExceptionTests
    {


        public async Task ThrowsException()
        {
            await Task.Delay(10);
            throw new Exception("Failure");
        }

        [TestMethod]
        public void Exception_Abandoned()
        {
            // Succeeds - Exception Lost
            ThrowsException();
        }

        [TestMethod]
        public async void Exception_AsyncVoid()
        {
            // Isn't recognized as a test
            await ThrowsException();
        }

        [TestMethod]
        public async Task Exception_AsyncTask()
        {
            // Succeeds - Exception Lost
            await ThrowsException();
        }
    }
}
