using BuildingBlock.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Modular.Core;
using Module.One.Controllers;

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
        public void Init(IServiceCollection services)
        {
            services.AddSingleton<IService, ModuleOneService>();
            services.AddTransient<ModuleOneController>();
        }

        public void MapEndpoints(IEndpointRouteBuilder endpoints)
        {
            endpoints.MapGet("/one", () =>
            {
                return Results.Ok("Module one");
            });

            endpoints.MapGet("/one/services", () =>
            {
                var serviceCollection = GlobalConfiguration.ModuleInitializers[this];
                var service = serviceCollection.BuildServiceProvider().GetRequiredService<IService>();

                if (service != null)
                {
                    return Results.Ok(service.SayHello());
                }
                return Results.NoContent();
            });
        }
    }
}
