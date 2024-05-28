using LibraryData.Interface;
using LibraryData.Models;
using LibraryData.Services;
using Microsoft.Azure.Functions.Worker;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var host = new HostBuilder()
    .ConfigureFunctionsWebApplication()
    .ConfigureServices((context, services) =>
    {
        services.AddApplicationInsightsTelemetryWorkerService();
        services.ConfigureFunctionsApplicationInsights();
        var configuration = context.Configuration;
        string connectionString = configuration.GetSection("ConnectionStrings:defaultConnection").Value!;
        services.AddDbContext<librarydbContext>(options => options.UseSqlServer(connectionString));
        services.AddScoped<IStudent, StudentService>();
        services.AddCors(options => options.AddPolicy("AllowAny", policy => policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()));
    })
    .Build();

host.Run();
