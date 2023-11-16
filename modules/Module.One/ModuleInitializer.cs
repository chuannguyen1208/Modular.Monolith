using BuildingBlock.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Modular.Core;

namespace Module.One
{
    internal class ModuleOneService : IService
    {
        public string SayHello()
        {
            return "Module One";
        }
    }

    public class ModuleInitializer : IModuleInitializer
    {
        public void MapEndpoints(IEndpointRouteBuilder endpoints)
        {
            endpoints
                .MapGet("/moduleOne/test", () => "Hello from module one")
                .WithTags("Module.One")
                .WithOpenApi();
        }
    }
}
