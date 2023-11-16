using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Modular.Core
{
    public class ModuleBasedControllerActivator : IControllerActivator
    {
        private IServiceScope _serviceScope = null!;

        public object Create(ControllerContext context)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));

            var controllerAssemblyName = context.ActionDescriptor.ControllerTypeInfo.Assembly.GetName().Name;

            IServiceCollection services = GlobalConfiguration.ServicesRoot;

            foreach (var (module, moduleServices) in GlobalConfiguration.ModuleInitializers)
            {
                var moduleAssemblyName = module.GetType().Assembly.GetName().Name;

                if (moduleAssemblyName == controllerAssemblyName)
                {
                    services = moduleServices;
                }
            }

            var serviceProvider = services.BuildServiceProvider();

            _serviceScope = serviceProvider.CreateScope();

            Type serviceType = context.ActionDescriptor.ControllerTypeInfo.AsType();

            return ActivatorUtilities.CreateInstance(_serviceScope.ServiceProvider, serviceType);
        }

        public void Release(ControllerContext context, object controller)
        {
            _serviceScope?.Dispose();
        }
    }
}
