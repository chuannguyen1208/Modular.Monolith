using BuildingBlock.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.One.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ModuleOneController : ControllerBase
    {
        [HttpGet]
        public string Get([FromServices] IServiceProvider serviceProvider)
        {
            var service = serviceProvider.GetRequiredService<IService>();
            return service.SayHello();
        }
    }
}
