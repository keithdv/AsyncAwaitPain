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
    [RoutePrefix("api/AsyncVoidException")]
    public class AsyncVoidExceptionController : ApiController
    {

        private string delayFinished = "Failure";

        [HttpGet]
        [Route("AsyncTaskThrowException")]
        public async Task<IHttpActionResult> AsyncTaskThrowException()
        {
            try
            {
                await DelayConfigureAwaitFalse();
            } catch (Exception ex)
            {
                return InternalServerError(ex);
            }

            return Ok("Success");
        }


        [HttpGet]
        [Route("WaitThrowException")]
        public IHttpActionResult WaitThrowException()
        {
            try
            {
                if (!DelayConfigureAwaitFalse().Wait(3000))
                {
                    delayFinished = "Blocked";
                }
            } catch(Exception ex)
            {
                return InternalServerError(ex);
            }

            return Ok(delayFinished);

        }

        [HttpGet]
        [Route("AsyncThrowException")]
        public IHttpActionResult AsyncThrowException()
        {
            try
            {
                DelayConfigureAwaitFalse();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }

            return Ok("Success (?)");
        }

        public async Task DelayConfigureAwaitFalse()
        {
            await Task.Delay(500).ConfigureAwait(false);
            throw new Exception("Failure");
        }

    }
}
