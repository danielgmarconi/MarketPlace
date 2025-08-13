using System.Globalization;
using MarketPlace.Infra.IoC;
using Microsoft.AspNetCore.Localization;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddLocalization(o => o.ResourcesPath = "Resources");


// Add services to the container.
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
        policy
            .WithOrigins("http://localhost:4200", "http://localhost:8080") // Angular via CLI ou Nginx
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials());
});

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddSwaggerGen();

var app = builder.Build();
var supported = new[] { "pt-BR", "en-US", "es-ES" };
var loc = new RequestLocalizationOptions()
    .SetDefaultCulture("pt-BR")
    .AddSupportedCultures(supported)
    .AddSupportedUICultures(supported);
loc.RequestCultureProviders.Clear();
loc.RequestCultureProviders.Add(new AcceptLanguageHeaderRequestCultureProvider());

app.UseRequestLocalization(loc);

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors(c => {  c.AllowAnyHeader(); 
                    c.AllowAnyMethod();
                    c.AllowAnyOrigin(); 
});
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
