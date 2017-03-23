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
    [RoutePrefix("api/ConfigureAwaitFalse")]
    public class ConfigureAwaitFalseController : ApiController
    {

        private string delayFinished = string.Empty;


        public async Task Delay()
        {
            delayFinished = "Failure";
            await Task.Delay(5).ConfigureAwait(false);
            delayFinished = "Completed successfully";
        }

        [HttpGet]
        [Route("Wait")]
        public IHttpActionResult Wait()
        {

            if (!Delay().Wait(50))
            {
                delayFinished = "Blocked";
            }

            return Ok(delayFinished);

        }

        [HttpGet]
        [Route("GetAwaiter")]
        public IHttpActionResult GetAwaiter()
        {

            Delay().GetAwaiter().GetResult();

            return Ok(delayFinished);

        }

        public async Task DelayNestedA()
        {
            delayFinished = "Failure";
            await DelayNestedB().ConfigureAwait(false);
        }

        public async Task DelayNestedB()
        {
            await Task.Delay(50);
            delayFinished = "Completed successfully";
        }

        [HttpGet]
        [Route("NestedConfigureAwait")]
        public IHttpActionResult NestedConfigureAwait()
        {

            if (!DelayNestedA().Wait(500))
            {
                delayFinished = "Blocked";
            }

            return Ok(delayFinished);

        }

    }
}
