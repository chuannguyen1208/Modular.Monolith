using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Modular.Core;
using Module.One;
using System.Reflection;
using System.Runtime.Loader;

var builder = WebApplication.CreateBuilder(args);

LoadInstalledModules(builder, out List<Assembly> assemblies);

builder.Host
    .UseServiceProviderFactory(new AutofacServiceProviderFactory())
    .ConfigureContainer<ContainerBuilder>(builder =>
    {
        builder.RegisterModule<ModuleOneModule>();
    });

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

foreach (var moduleInitializer in GlobalConfiguration.Initializers)
{
    moduleInitializer.MapEndpoints(app);
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

static void LoadInstalledModules(WebApplicationBuilder builder, out List<Assembly> assemblies)
{
    assemblies = new List<Assembly>();

    var moduleRootFolder = new DirectoryInfo(builder.Environment.ContentRootPath);
    var moduleFolders = moduleRootFolder.GetDirectories().Where(f => f.FullName.EndsWith("bin"));

    foreach (var moduleFolder in moduleFolders)
    {
        var moduleFiles = moduleFolder.GetFileSystemInfos("Module.*.dll", SearchOption.AllDirectories);

        foreach (var file in moduleFiles)
        {
            Assembly? assembly = null;

            try
            {
                assembly = AssemblyLoadContext.Default.LoadFromAssemblyPath(file.FullName);
                assemblies.Add(assembly);

                var moduleInitializerType = assembly
                    .GetTypes()
                    .Where(x => typeof(IModuleInitializer).IsAssignableFrom(x))
                    .FirstOrDefault();

                if (moduleInitializerType != null && moduleInitializerType != typeof(IModuleInitializer))
                {
                    var moduleInitializer = (IModuleInitializer)Activator.CreateInstance(moduleInitializerType)!;

                    GlobalConfiguration.Initializers.Add(moduleInitializer);
                }
            }
            catch
            {
                // do nothing
            }
        }
    }
}
