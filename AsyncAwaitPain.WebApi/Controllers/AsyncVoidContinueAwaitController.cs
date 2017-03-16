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
    [RoutePrefix("api/AsyncVoidContinueAwait")]
    public class AsyncVoidContinueAwaitController : ApiController
    {

        private string delayFinished = "Failure";

        public async Task Delay()
        {
            await Task.Delay(500);
            delayFinished = "Completed successfully";
        }

        [HttpGet]
        [Route("Wait")]
        public IHttpActionResult Get()
        {

            if (!Delay().Wait(3000))
            {
                delayFinished = "Blocked";
            }

            return Ok(delayFinished);
        }

        [HttpGet]
        [Route("ConfigureAwaitFalse1")]
        public IHttpActionResult ConfigureAwaitFalse1()
        {

            Delay().ConfigureAwait(false).GetAwaiter().GetResult();

            return Ok(delayFinished);

        }

        [HttpGet]
        [Route("ConfigureAwaitFalse2")]
        public IHttpActionResult ConfigureAwaitFalse2()
        {

            var t = Delay();
            t.ConfigureAwait(false);
            if (!t.Wait(3000))
            {
                delayFinished = "Blocked";
            }

            return Ok(delayFinished);

        }
        

        public async Task DelayConfigureAwaitFalse()
        {
            await Task.Delay(500).ConfigureAwait(false);
            delayFinished = "Completed successfully";
        }

        [HttpGet]
        [Route("ConfigureAwaitFalse3")]
        public IHttpActionResult ConfigureAwaitFalse3()
        {

            if (!DelayConfigureAwaitFalse().Wait(3000))
            {
                delayFinished = "Blocked";
            }

            return Ok(delayFinished);

        }



    }
}
