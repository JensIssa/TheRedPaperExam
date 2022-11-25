using System.Runtime.Serialization;
using Application.InterfaceRepos;
using Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.DependencyResolvers;

public class DepedencyResolver
{
    public static void RegisterInfrastructure(IServiceCollection services)
    {
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<ICategoryRepository, CategoryRepository>();

    }
}