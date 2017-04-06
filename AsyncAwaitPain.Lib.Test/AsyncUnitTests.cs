using AsyncAwaitPain.Lib.AsyncUnitTest;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsyncAwaitPain.Lib.Test
{
    [TestClass]
    public class AsyncUnitTests
    {

        [TestMethod]
        public async Task MoqRaiseEvent()
        {
            // Doesn't work
            // Mock.Raise doesn't return the task!

            var mockCollection = new Mock<IObservableCollectionAsync<string>>();

            Action raiseEvent = () => mockCollection.Raise(e => e.CollectionChangedAsync += null, (NotifyCollectionChangedEventArgs)null);

            mockCollection.Setup(x => x.AddAsync(It.IsAny<string>()))
                .Callback(() => { raiseEvent(); })
                .Returns(Task.CompletedTask);

            var bo = new BusinessObjectAsync(mockCollection.Object);

            await bo.OperationAsync();
            await bo.OperationAsync();
            await bo.OperationAsync();

            Assert.AreEqual(3, bo.CompletedCount);

        }
    }
}
