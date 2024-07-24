using LibraryData.Interface;
using LibraryData.Models;
using LibraryData.Services;
using LibraryData.Services.BookServices;
using LibraryData.Utilities;
using Microsoft.AspNetCore.Builder;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Extensions.OpenApi.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var host = new HostBuilder()
    .ConfigureFunctionsWebApplication()
    .ConfigureOpenApi()
    .ConfigureServices((context, services) =>
    {
        services.AddApplicationInsightsTelemetryWorkerService();
        services.ConfigureFunctionsApplicationInsights();
        var configuration = context.Configuration;
        string connectionString = configuration.GetSection("ConnectionStrings:defaultConnection").Value!;
        services.AddDbContext<LibrarydbContext>(options => options.UseSqlServer(connectionString, serverOptions =>
        {
            serverOptions.EnableRetryOnFailure();
        }));
        services.AddScoped<IStudent, StudentService>();
        services.AddScoped<ILibrarian, LibrarianService>();
        services.AddSingleton<IScopedDbContext<LibrarydbContext>, ScopedDbContext<LibrarydbContext>>();
        services.AddTransient<ReportService>();
        services.AddSingleton<FunctionService>();
        services.AddSingleton<PerformanceAnalyzer>();
        services.AddScoped<IBook, BookCRUD>();
        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        services.AddCors(options => options.AddPolicy("AllowAny", policy => policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()));
    })
    .Build();

LibrarianMapper.Initialize(host.Services);

host.Run();
