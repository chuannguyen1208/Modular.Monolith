using Autofac;
using BuildingBlock.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.Two
{
    public class ModuleTwoModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder
                .RegisterType<ModuleTwoService>()
                .As<IService>()
                .SingleInstance();
        }
    }
}
