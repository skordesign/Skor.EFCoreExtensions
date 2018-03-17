using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Skor.EFCoreExtensions.Repositories;

namespace Skor.EFCoreExtensions.DI
{
    public static class DI
    {
        public static void AddSingletonRepository<TModel, TDbContext>(this IServiceCollection services)
            where TModel : class,new()
            where TDbContext : DbContext
        {
            services.AddSingleton<IBaseRepository<TModel>, BaseRepository<TModel, TDbContext>>();
        }
        public static void AddTransientRepository<TModel, TDbContext>(this IServiceCollection services)
            where TModel : class, new()
            where TDbContext : DbContext
        {
            services.AddTransient<IBaseRepository<TModel>, BaseRepository<TModel, TDbContext>>();
        }
        public static void AddScopedRepository<TModel, TDbContext>(this IServiceCollection services)
            where TModel : class, new()
            where TDbContext : DbContext
        {
            services.AddScoped<IBaseRepository<TModel>, BaseRepository<TModel, TDbContext>>();
        }
    }
}
