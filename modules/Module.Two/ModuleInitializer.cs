using BuildingBlock.Services;
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
        public void Init(IServiceCollection services)
        {
            services.AddSingleton<IService, ModuleTwoService>();
        }

        public void MapEndpoints(IEndpointRouteBuilder endpoints)
        {
        }
    }
}
