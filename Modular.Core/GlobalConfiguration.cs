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
        public static List<IModuleInitializer> Initializers { get; set; } = new List<IModuleInitializer>();
    }
}
