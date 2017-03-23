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

        [HttpGet]
        [Route("AsyncTask")]
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
        [Route("Wait")]
        public IHttpActionResult Wait()
        {
            try
            {
                if (!DelayConfigureAwaitFalse().Wait(100))
                {
                    delayFinished = "Blocked";
                }
            } catch(Exception ex)
            {
                return InternalServerError(ex);
            }

            return Ok(delayFinished);

        }

        public async Task DelayConfigureAwaitFalse()
        {
            await Task.Delay(50).ConfigureAwait(false);
            throw new Exception("Failure");
        }

        [HttpGet]
        [Route("Abandon")]
        public IHttpActionResult Abandon()
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



    }
}
