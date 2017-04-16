using AsyncAwaitPain.Lib;
using AsyncAwaitPain.Lib.Constructor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace AsyncAwaitPain.WebApi.Controllers
{
    [AllowAnonymous]
    [RoutePrefix("api/AsyncConstructor")]

    public class AsyncConstructorController : ApiController
    {

        [HttpGet]
        [Route("New")]
        public IHttpActionResult New()
        {
            var o = new AsyncConstructor();

            // Even if this was an Async Task<> there's nothing to await

            return Ok(o.Message);
        }

        [HttpGet]
        [Route("Yield")]
        public async Task<IHttpActionResult> Yield()
        {
            var o = new AsyncConstructor();

            // NOT RECOMMENDED!!!
            // Remember, async doesn't create another thread
            // So this thread needs to be assigned to complete the 'Delay' task
            // Yield appears to do the trick
            // But, like so many cases, the issue is the exception is lost!

            for (var i = 0; i < 100 && o.Completed == false; i++) // Infinite loop
            {
                await Task.Yield();
                await Task.Delay(10);
            }

            return Ok(o.Message);
        }

        [HttpGet]
        [Route("Exception")]
        public IHttpActionResult Exception()
        {
            var o = new AsyncConstructorException();

            return Ok(o.Message);
        }


        [HttpGet]
        [Route("YieldException")]
        public async Task<IHttpActionResult> YieldException()
        {
            var o = new AsyncConstructorException();

            for (var i = 0; i < 100 && o.Completed == false; i++) // Infinite loop
            {
                await Task.Yield();
                await Task.Delay(10);
            }

            if (o.Completed)
            {
                return Ok("Completed");
            } else
            {
                return Ok("Failed: Not completed an no exception raised");
            }
        }

        [HttpGet]
        [Route("Initialize")]
        public async Task<IHttpActionResult> Initialize()
        {
            var o = new AsyncConstructorInitialize();

            // Recommended way
            // Easy to link the task
            await o.InitializeAsync();

            return Ok(o.Message);
        }

        [HttpGet]
        [Route("InitializeException")]
        public async Task<IHttpActionResult> InitializeException()
        {
            var o = new AsyncConstructorInitializeException();

            // Recommended way
            // Easy to link the task
            await o.InitializeAsync();

            return Ok(o.Message);
        }
    }
}