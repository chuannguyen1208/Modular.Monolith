using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;

namespace Modular.Core
{
    public interface IModuleInitializer
    {
        void Init(IServiceCollection services);
        void MapEndpoints(IEndpointRouteBuilder endpoints);
    }
}