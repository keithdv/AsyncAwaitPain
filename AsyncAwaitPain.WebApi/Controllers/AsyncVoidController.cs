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
    [RoutePrefix("api/AsyncVoid")]
    public class AsyncVoidController : ApiController
    {
        private string delayFinished = "Failure";

        [Route("Sync")]
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

        public async Task Delay()
        {
            await Task.Delay(500);
            delayFinished = "Completed successfully";
        }

    }
}
