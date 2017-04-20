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
    [RoutePrefix("api/Intro")]
    public class IntroController : ApiController
    {
        private string delayFinished;

        public async Task Delay()
        {
            delayFinished = "Failure";
            await Task.Delay(TimeConstants._1second);
            delayFinished = "Completed successfully";
        }

        [Route("Abandon")]
        [HttpGet]
        public IHttpActionResult Get()
        {
            // Wrong: Completes before the Task.Delay
            Delay();

            return Ok(delayFinished);
        }

        [Route("AsyncVoid")]
        [HttpGet]
        public async void AsyncVoid()
        {
            // Error: An asynchronous module or handler completed while an asynchronous operation was still pending.
            await Delay();

        }

        [Route("AsyncTask")]
        [HttpGet]
        public async Task<IHttpActionResult> AsyncTask()
        {
            // Success
            await Delay();

            return Ok(delayFinished);

        }


    }
}
