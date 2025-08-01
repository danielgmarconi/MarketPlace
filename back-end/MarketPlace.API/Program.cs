using MarketPlace.Infra.IoC;
var builder = WebApplication.CreateBuilder(args);

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

// Configure the HTTP request pipeline.
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
