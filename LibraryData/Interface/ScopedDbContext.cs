using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace LibraryData.Interface
{
    public interface IScopedDbContext<T> where T : DbContext
    {
        DbContextScope<T> GetDbContextScope();
    }

    public class ScopedDbContext<T> : IScopedDbContext<T> where T : DbContext
    {
        readonly IServiceProvider serviceProvider;

        public ScopedDbContext(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
        }

        public DbContextScope<T> GetDbContextScope()
        {
            var scope = serviceProvider.CreateScope();
            return new DbContextScope<T>(scope);
        }
    }

        public class DbContextScope<T> : IDisposable where T : DbContext
        {
            readonly IServiceScope scope;
            public T DbContext { get; private set; }

            public DbContextScope(IServiceScope scope)
            {
                this.scope = scope ?? throw new ArgumentNullException(nameof(scope));
                DbContext = scope.ServiceProvider.GetRequiredService<T>();
            }

            public void Dispose()
            {
                scope.Dispose();
            }
        }
}
