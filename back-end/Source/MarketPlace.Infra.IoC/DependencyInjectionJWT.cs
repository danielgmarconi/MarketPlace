﻿using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace MarketPlace.Infra.IoC;

public static class DependencyInjectionJWT
{
    public static IServiceCollection AddInfrastructureJWT(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAuthentication(opt =>
        {
            opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(opt => { opt.TokenValidationParameters = new TokenValidationParameters { ValidateIssuer = true,
                                                                                               ValidateAudience = true,
                                                                                               ValidateLifetime = true,
                                                                                               ValidateIssuerSigningKey = true,
                                                                                               ValidIssuer = configuration["Jwt:Issuer"],
                                                                                               ValidAudience = configuration["Jwt:Audience"],
                                                                                               IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Secretkey"])),
                                                                                               ClockSkew = TimeSpan.Zero
                                                                                               };
        });
        return services;
    }
}
