using AsyncAwaitPain.Lib;
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

            return Ok(o.Message);
        }

        [HttpGet]
        [Route("WorkDammit")]
        public IHttpActionResult WorkDammit()
        {
            var o = new AsyncConstructor();

            System.Threading.Thread.Yield();
            System.Threading.Thread.Sleep(200);
            
            while(o.Completed == false)
            {
                System.Threading.Thread.Yield();
                System.Threading.Thread.Sleep(200);
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
    }
}