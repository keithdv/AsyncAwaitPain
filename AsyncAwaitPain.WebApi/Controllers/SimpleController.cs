using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Results;

namespace AsyncAwaitPain.WebApi.Controllers
{
    [AllowAnonymous]
    [RoutePrefix("api/Simple")]
    public class SimpleController : ApiController
    {
        private string delayFinished;

        [Route("Abandon")]
        [HttpGet]
        public IHttpActionResult Get()
        {

            Delay();

            return Ok(delayFinished);
        }



        [Route("Async")]
        [HttpGet]
        public async Task<IHttpActionResult> GetAsync()
        {

            await Delay();

            return Ok(delayFinished);

        }

        [HttpGet]
        [Route("Wait")]
        public IHttpActionResult Wait()
        {

            if (!Delay().Wait(100))
            {
                delayFinished = "Blocked";
            }

            return Ok(delayFinished);
        }

        [HttpGet]
        [Route("GetAwaiter")]
        public IHttpActionResult GetAwaiter()
        {

            Delay().ConfigureAwait(false).GetAwaiter().GetResult();

            return Ok(delayFinished);

        }

        public async Task Delay()
        {
            delayFinished = "Failure";
            await Task.Delay(50);
            delayFinished = "Completed successfully";
        }

    }
}
