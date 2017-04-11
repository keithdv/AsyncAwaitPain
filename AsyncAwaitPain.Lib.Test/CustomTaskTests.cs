using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsyncAwaitPain.Lib.Test
{

    public class CustomTask
    {
        public Exception Exception { get; set; }
    }

    [TestClass]
    public class CustomTaskTests
    {

        private CustomTask MethodAsync()
        {
            CustomTask task = new CustomTask();

            try
            {
                throw new Exception("Example");
            }
            catch (Exception ex)
            {
                task.Exception = ex;
            }
            return task;
        }

        private void AsyncVoid()
        {
            MethodAsync();
        }

        [TestMethod]
        public void AsyncVoidTest()
        {
            // Nothing is managing the returned ExceptionTask
            // So the exception is never handled
            // The exception has "disappeared"
            AsyncVoid();

            // Same as Task
            // “When an exception is thrown out of an async Task or async Task<T> method, that exception is captured and placed on the Task object.”
            // Async / Await - Best Practices in Asynchronous Programming
            // Stephen Clearly

        }

    }
}
