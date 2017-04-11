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
    [RoutePrefix("api/Exception")]
    public class ExceptionController : ApiController
    {

        private string delayFinished = "Failure";

        public async Task DelayThrowException()
        {
            await Task.Delay(TimeConstants._25milliseconds).ConfigureAwait(false);
            throw new Exception("Failure");
        }

        [HttpGet]
        [Route("AsyncTask")]
        public async Task<IHttpActionResult> AsyncTask()
        {
            // Works perfectly
            await DelayThrowException();

            return Ok("Success");
        }


        [HttpGet]
        [Route("Wait")]
        public IHttpActionResult Wait()
        {
            // Works perfectly
            if (!DelayThrowException().Wait(TimeConstants._500milliseconds))
            {
                delayFinished = "Blocked";
            }

            return Ok(delayFinished);

        }



        [HttpGet]
        [Route("Abandon")]
        public IHttpActionResult Abandon()
        {
            // No exception show?!
            DelayThrowException();

            // Where did it go?
            // Put a breakpoint in Global.Asax.cs=>TaskScheduler_UnobservedTaskException
            // And call GarbageCollector.Collect
            // to see!!

            return Ok("Success (?)");

        }

        [HttpGet]
        [Route("AsyncVoid")]
        public async void AsyncVoid()
        {

            await DelayThrowException();

        }

    }
}
