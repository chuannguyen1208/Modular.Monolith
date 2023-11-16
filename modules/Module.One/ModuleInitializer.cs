using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Modular.Core;

namespace Module.One
{
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
