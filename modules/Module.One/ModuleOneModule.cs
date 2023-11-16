using Autofac;
using BuildingBlock.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.One
{
    public class ModuleOneModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder
                .RegisterType<ModuleOneService>()
                .As<IService>()
                .SingleInstance();
        }
    }
}
