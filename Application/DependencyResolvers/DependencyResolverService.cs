using Application.Helpers;
using Application.InterfaceServices;
using Microsoft.Extensions.DependencyInjection;

namespace Application.DependencyResolvers;

public class DependencyResolverService
{
    public static void RegisterApplicationLayer(IServiceCollection service)
    {
        service.AddScoped<TokenGenerator>();
    }
}