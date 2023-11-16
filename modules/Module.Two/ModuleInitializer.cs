using BuildingBlock.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Modular.Core;

namespace Module.Two
{
    internal class ModuleTwoService : IService
    {
        public string SayHello()
        {
            return "Module Two";
        }
    }

    public class ModuleInitializer : IModuleInitializer
    {
        public void MapEndpoints(IEndpointRouteBuilder endpoints)
        {
            endpoints
                .MapGet("/moduleTwo/test", () => "Hello from module two")
                .WithTags("Module.Two")
                .WithOpenApi();
        }
    }
}
