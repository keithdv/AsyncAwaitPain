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
            await Task.Delay(TimeConstants._500milliseconds);
            delayFinished = "Completed successfully";
        }

        [HttpGet]
        [Route("AsyncTask")]
        public async Task<IHttpActionResult> AsyncTask()
        {
            // Correct - no issues
            await Delay();
            return Ok(delayFinished);
        }

        [HttpGet]
        [Route("Wait")]
        public IHttpActionResult Wait()
        {
            // I'm not yielding I'm blocking
            if (!Delay().Wait(TimeConstants._5seconds)) // If I don't provide a timeout forever deadlocked
            {
                delayFinished = "Deadlock"; // Deadlock - on completion task attempts to post on blocked thread - Deadlock
            }

            return Ok(delayFinished);
        }

        public Task DelayTask()
        {
            delayFinished = "Failure";
            return Task.Delay(TimeConstants._500milliseconds).ContinueWith(x =>
            {
                delayFinished = "Completed successfully";
            });
        }

        [HttpGet]
        [Route("WaitTask")]
        public IHttpActionResult WaitTask()
        {
            // Still blocks even when Async/Await isn't involved

            if (!Delay().Wait(TimeConstants._5seconds)) // If I don't provide a timeout forever deadlocked
            {
                delayFinished = "Deadlock"; // Deadlock - on completion task attempts to post on blocked thread - Deadlock
            }

            return Ok(delayFinished);
        }

        /// <summary>
        ///  Is ConfigureAwait the answer??
        /// </summary>
        /// <returns></returns>
        public async Task DelayConfigureAwaitFalse()
        {
            delayFinished = "Failure";
            await Task.Delay(TimeConstants._1second).ConfigureAwait(false);
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
            await Task.Delay(TimeConstants._1second).ConfigureAwait(false);
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
