using BuildingBlock.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebHost.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SamplesController : ControllerBase
    {

        [HttpGet]
        public string Get([FromServices] IService service)
        {
            return service.SayHello();
        }
    }
}
