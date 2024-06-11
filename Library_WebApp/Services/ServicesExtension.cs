using Library_WebApp.Services.HttpServices;

namespace Library_WebApp.Services
{
    public static class ServicesExtention
    {
        public static IServiceCollection AddHttpServices(this IServiceCollection services)
        {
            services.AddHttpClient();
            services.AddScoped<IStudentCRUD, StudentCRUD>();
            services.AddScoped<IStudentService, StudentService>();
            services.AddScoped<ILibrarianCRUD, LibrarianCRUD>();
            services.AddScoped<ILibrarianService, LibrarianService>();
            services.AddScoped<UserStateManagementService>();
            return services;
        }
    }
}