using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace MyTravel.API.Controllers
{
    [Route("api/shoudongapi")]
    //[Controller]
    //public class ShoudongAPIController
    public class ShoudongAPI : Controller

    {
        // GET: api/<TestAPIController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }
    }
}
