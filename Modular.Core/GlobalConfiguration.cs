using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modular.Core
{
    public static class GlobalConfiguration
    {
        static GlobalConfiguration()
        {
            ModuleInitializers = new Dictionary<IModuleInitializer, IServiceCollection> ();
        }

        public static IServiceCollection ServicesRoot { get; set; } = null!;
        public static Dictionary<IModuleInitializer, IServiceCollection> ModuleInitializers { get; set; }
    }
}
