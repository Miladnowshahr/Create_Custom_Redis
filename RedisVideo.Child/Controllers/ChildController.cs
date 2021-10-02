using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RedisVideo.Child.Controllers
{
    [ApiController]
    [Route("partition")]
    public class ChildController : ControllerBase
    {

        private readonly ILogger<ChildController> _logger;

        public ChildController(ILogger<ChildController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public ActionResult<string> Get([FromQuery] string key,[FromQuery] unit hashkey)
        {

        }

        [HttpPost]
        public IActionResult Add([FromBody] EntryDto entry)
        {

        }
    }
}
