using Microsoft.Extensions.DependencyInjection;
using TestApp.Models.ExplorerModels;
using TestApp.ViewModels;

namespace TestApp;

public static class ServiceCollectionExtension
{
    public static void AddCommonServices(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddTransient<MainWindowViewModel>();
        serviceCollection.AddScoped<IPathValidator, PathValidator>();
        serviceCollection.AddScoped<IExplorerService, ExplorerService>();
    }
}