using Application.Interfaces.Repositories;
using Application.Services;
using Domain.Interfaces;
using Infrastructure.Persistence.Dapper;

using Scalar.AspNetCore;
using Serilog;
var builder = WebApplication.CreateBuilder(args);
Console.WriteLine("Cadena: " + builder.Configuration.GetConnectionString("MiServer"));
// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddOpenApi();

// Serilog
Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.File("logs/agendamedica.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();

builder.Host.UseSerilog();

// Registrar dependencias ANTES de Build
//Una sola instancia para toda la aplicación
builder.Services.AddSingleton<DapperContext>();
//Una instancia por cada petición HTTP
builder.Services.AddScoped<IPacienteRepository, PacienteRepository>();
builder.Services.AddScoped<IPacienteService, PacienteService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference(options =>
    {
        options.WithOpenApiRoutePattern("/openapi/v1.json");
        options.WithTitle("Mi API");
    });
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();