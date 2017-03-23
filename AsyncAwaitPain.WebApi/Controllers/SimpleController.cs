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

        public async Task Delay()
        {
            delayFinished = "Failure";
            await Task.Delay(50);
            delayFinished = "Completed successfully";
        }

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

            if (!Delay().Wait(25)) // Use Constants
            {
                delayFinished = "Deadlock"; // Deadlock - both threads trying to access the same ExecutionContext
            }

            return Ok(delayFinished);
        }


    }
}
