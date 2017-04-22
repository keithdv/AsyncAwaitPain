using AsyncAwaitPain.Lib.AsyncEvent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace AsyncAwaitPain.WebApi.Controllers
{


    [RoutePrefix("api/AsyncEvent")]
    public class AsyncEventController : ApiController
    {

        [HttpGet]
        [Route("SyncOperation")]
        public IHttpActionResult SyncOperation()
        {
            var bo = new BusinessObject();

            bo.Operation();

            return Ok($"Completed Count: {bo.CompletedCount}");
        }

        [HttpGet]
        [Route("AsyncOperation")]
        public async Task<IHttpActionResult> AsyncOperation()
        {
            var bo = new BusinessObjectAsync();

            await bo.OperationAsync();

            return Ok($"Completed Count: {bo.CompletedCount}");
        }

        [HttpGet]
        [Route("SyncOperationException")]
        public IHttpActionResult SyncOperationException()
        {
            var bo = new BusinessObjectException();

            bo.Operation();

            return Ok($"Completed Count: {bo.CompletedCount}");
        }

        [HttpGet]
        [Route("AsyncOperationException")]
        public async Task<IHttpActionResult> AsyncOperationException()
        {
            var bo = new BusinessObjectAsyncException();

            await bo.OperationAsync();

            return Ok($"Completed Count: {bo.CompletedCount}");
        }


    }
}
