
using DataAccessLayer.SqlServerAdapter;
using MarketPlace.Application.Interfaces;
using MarketPlace.Application.Mappings;
using MarketPlace.Application.Services;
using MarketPlace.Domain.Interfaces;
using MarketPlace.Infra.Data.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MarketPlace.Infra.IoC;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var defaultConnection = configuration.GetConnectionString("DefaultConnection");
        services.AddScoped<ISQLServerAdapter>(_ => new SQLServerAdapter(defaultConnection));
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IUserService, UserService>();

        services.AddAutoMapper(typeof(DomainToDTOMappingProfile));
        return services;
    }
}
