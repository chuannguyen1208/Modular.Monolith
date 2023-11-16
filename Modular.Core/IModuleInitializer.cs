using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;

namespace Modular.Core
{
    public interface IModuleInitializer
    {
        void MapEndpoints(IEndpointRouteBuilder endpoints);
    }
}