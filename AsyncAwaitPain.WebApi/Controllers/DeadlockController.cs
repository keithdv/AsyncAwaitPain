using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Results;

namespace AsyncAwaitPain.WebApi.Controllers
{
    [AllowAnonymous]
    [RoutePrefix("api/Deadlock")]
    public class DeadlockController : ApiController
    {

        private string delayFinished = string.Empty;

        public async Task Delay()
        {
            delayFinished = "Failure";
            await Task.Delay(TimeConstants._25milliseconds);
            delayFinished = "Completed successfully";
        }

        [HttpGet]
        [Route("AsyncMethod")]
        public async Task<IHttpActionResult> AsyncMethod()
        {
            await Delay();

            return Ok(delayFinished);
        }

        [HttpGet]
        [Route("Wait")]
        public IHttpActionResult Wait()
        {

            if (!Delay().Wait(TimeConstants._5seconds)) // If I don't provide a timeout forever deadlocked
            {
                delayFinished = "Deadlock"; // Deadlock - both threads trying to access the same context
            }

            return Ok(delayFinished);
        }

        public async Task DelayConfigureAwaitFalse()
        {
            delayFinished = "Failure";
            await Task.Delay(TimeConstants._25milliseconds).ConfigureAwait(false);
            delayFinished = "Completed successfully";
        }

        [HttpGet]
        [Route("Wait_ConfigureAwaitFalse")]
        public IHttpActionResult Wait_ConfigureAwaitFalse()
        {
            // ConfigureAwait false
            // No deadlock since a new context is created

            if (!DelayConfigureAwaitFalse().Wait(TimeConstants._5seconds))
            {
                delayFinished = "Deadlock"; // Deadlock - both threads trying to access the same context
            }

            // At this point we are in a new context
            return Ok(delayFinished);
        }

        public async Task DelayConfigureAwaitFalseNestedA()
        {
            delayFinished = "Failure";
            await DelayConfigureAwaitFalseNestedB().ConfigureAwait(false);
        }

        public async Task DelayConfigureAwaitFalseNestedB()
        {
            await Task.Delay(50);
            delayFinished = "Completed successfully";
        }

        [HttpGet]
        [Route("NestedConfigureAwait")]
        public IHttpActionResult NestedConfigureAwait()
        {
            // ConfigureAwait is not defined all the way down the async "tree"
            // Thus this will block
            // Hence, Stephen Clearly's advice to "In your “library” async methods, use ConfigureAwait(false) wherever possible."
            // https://blog.stephencleary.com/2012/07/dont-block-on-async-code.html

            if (!DelayConfigureAwaitFalseNestedA().Wait(TimeConstants._5seconds))
            {
                delayFinished = "Deadlock";
            }

            return Ok(delayFinished);

        }

    }
}
