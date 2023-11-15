using BuildingBlock.Services;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Modular.Core;
using Module.One.Controllers;
using System.Reflection;
using System.Runtime.Loader;
using WebHost.Controllers;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.

LoadInstalledModules(builder);


var moduleInitializerInterface = typeof(IModuleInitializer);

List<Assembly> loadedModuleAssemblies = new();

foreach (var (moduleInitializer, moduleServiceCollection) in GlobalConfiguration.ModuleInitializers)
{
    moduleInitializer.Init(moduleServiceCollection);
    loadedModuleAssemblies.Add(moduleInitializer.GetType().Assembly);
}

builder.Services.AddSingleton<IService, MainService>();
builder.Services.Replace(ServiceDescriptor.Singleton<IControllerActivator, ModuleBasedControllerActivator>());

var mvcBuilder = builder.Services.AddControllers();

foreach (var assembly in loadedModuleAssemblies)
{
    mvcBuilder.AddApplicationPart(assembly);
}

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

foreach (var (moduleInitializer, _) in GlobalConfiguration.ModuleInitializers)
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

static void LoadInstalledModules(WebApplicationBuilder builder)
{
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

                var moduleInitializerType = assembly
                    .GetTypes()
                    .Where(x => typeof(IModuleInitializer).IsAssignableFrom(x))
                    .FirstOrDefault();

                if (moduleInitializerType != null && moduleInitializerType != typeof(IModuleInitializer))
                {
                    var moduleInitializer = (IModuleInitializer)Activator.CreateInstance(moduleInitializerType)!;

                    GlobalConfiguration.ModuleInitializers.Add(moduleInitializer, builder.Services.CreateNewServiceCollection());
                }
            }
            catch (FileLoadException)
            {
                // Get loaded assembly
                assembly = Assembly.Load(new AssemblyName(Path.GetFileNameWithoutExtension(file.Name)));

                if (assembly == null)
                {
                    throw;
                }
            }
        }
    }

    GlobalConfiguration.ServicesRoot = builder.Services;
}

internal class MainService : IService
{
    public string SayHello()
    {
        return "Main";
    }
}
