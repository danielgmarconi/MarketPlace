
using System.Reflection;
using DataAccessLayer.SqlServerAdapter;
using FluentValidation;
using MarketPlace.Application.Interfaces;
using MarketPlace.Application.Mappings;
using MarketPlace.Application.Services;
using MarketPlace.Domain.Interfaces;
using MarketPlace.Infra.Data.Repositories;
using MarketPlace.Infra.Encryption;
using MarketPlace.Infra.Jwt;
using MarketPlace.Infra.Jwt.Service;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;




namespace MarketPlace.Infra.IoC;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddInfrastructureJWT(configuration);
        services.AddScoped<IJwtService, JwtService>();

        services.AddValidatorsFromAssembly(Assembly.Load("MarketPlace.Application"));
        services.AddScoped<ISQLServerAdapter>(_ => new SQLServerAdapter(configuration.GetConnectionString("DefaultConnection")));
        services.AddScoped<IEncryptionService>(x => new EncryptionService(configuration["Secretkey"]));
        services.AddAutoMapper(typeof(DomainToDTOMappingProfile));

        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IUserService, UserService>();

        return services;
    }
}
