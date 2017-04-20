using AsyncAwaitPain.Lib.CSLA;
using Csla;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsyncAwaitPain.Lib.Test
{

    [TestClass]
    public class CslaBusinessObjectTests
    {

        [TestMethod]
        public async Task Fetch()
        {
            var result = await DataPortal.FetchAsync<CslaBusinessObjectList>();

            Assert.AreEqual(10, result.Count);
            Assert.IsTrue(result.Where(x => string.IsNullOrWhiteSpace(x.Name)).Count() == 0);
        }

        [TestMethod]
        public async Task FetchException()
        {
            Exception ex = null;

            try
            {
                var result = await DataPortal.FetchAsync<CslaBusinessObjectExceptionList>();

            }
            catch (Exception ex2)
            {
                ex = ex2;
            }

            Assert.IsNotNull(ex);

        }
    }
}
