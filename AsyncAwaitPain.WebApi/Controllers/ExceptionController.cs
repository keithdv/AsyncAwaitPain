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
            await Task.Delay(TimeConstants._500milliseconds).ConfigureAwait(false); // ConfigureAwaitFalse - Won't block
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
        [Route("AsyncTaskTryCatch")]
        public async Task<IHttpActionResult> AsyncTaskTryCatch()
        {
            // Works perfectly

            try
            {
                await DelayThrowException();
            }
            catch (Exception)
            {
                return Ok("Caught");
            }

            return Ok("Success");
        }

        [HttpGet]
        [Route("Abandon")]
        public IHttpActionResult Abandon()
        {
            DelayThrowException();

            // False positive and the exception is lost 
            return Ok("Success!???");

        }


        [HttpGet]
        [Route("AbandonTryCatch")]
        public IHttpActionResult AbandonTryCatch()
        {
            try
            {
                DelayThrowException();
            }
            catch (Exception)
            {
                return Ok("Sucess: Caught");
            }

            // Exception is not caught
            return Ok("Failure: Not caught");
        }

        [HttpGet]
        [Route("Wait")]
        public IHttpActionResult Wait()
        {
            // Works because of ConfigureAwait(false)
            if (!DelayThrowException().Wait(TimeConstants._1second))
            {
                delayFinished = "Blocked";
            }

            return Ok(delayFinished);

        }

        [HttpGet]
        [Route("WaitTryCatch")]
        public IHttpActionResult WaitTryCatch()
        {
            // Works because of ConfigureAwait(false)

            try
            {
                if (!DelayThrowException().Wait(TimeConstants._500milliseconds))
                {
                    delayFinished = "Blocked";
                }
            }
            catch (Exception)
            {
                return Ok("Sucess: Caught");
            }


            return Ok("Failure: Not Caught");

        }

        [HttpGet]
        [Route("AsyncVoid")]
        public async void AsyncVoid()
        {
            await DelayThrowException();
        }



    }
}
