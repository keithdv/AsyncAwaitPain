using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace AsyncAwaitPain.WebApi.Controllers
{


    [RoutePrefix("api/garbageCollector")]
    public class GarbageCollectorController : ApiController
    {

        [HttpGet]
        [Route("Collect")]
        public void Collect()
        {
            GC.Collect();
        }

    }
}