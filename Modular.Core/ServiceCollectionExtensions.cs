using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modular.Core
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection CreateNewServiceCollection(this IServiceCollection services)
        {
            ServiceCollection newServiceCollection = new ServiceCollection();

            foreach (var service in services)
            {
                newServiceCollection.Add(service);
            }

            newServiceCollection.RemoveAll<IHostedService>();

            return newServiceCollection;
        }
    }
}
