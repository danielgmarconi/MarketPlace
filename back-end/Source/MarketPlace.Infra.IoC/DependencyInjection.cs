using MarketPlace.Application.Interfaces;
using MarketPlace.Application.Mappings;
using MarketPlace.Application.Services;
using MarketPlace.Domain.Interfaces;
using MarketPlace.Infra.Data.Context;
using MarketPlace.Infra.Data.Repositories;
using MarketPlace.Infra.Jwt.Context;
using MarketPlace.Infra.Jwt.Service;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MarketPlace.Infra.IoC;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
                                                                                    b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IJwtService>(x => { return new JwtService(new ApplicationJwtContext() { SecretKey = configuration["Jwt:Secretkey"], 
                                                                                                   Issuer = configuration["Jwt:Issuer"], 
                                                                                                   Audience = configuration["Jwt:Audience"], 
                                                                                                   ExpiresMinutes = int.Parse(configuration["Jwt:ExpiresMinutes"])}); 
                                       });

        services.AddAutoMapper(typeof(DomainToDTOMappingProfile));

        return services;
    }
}
