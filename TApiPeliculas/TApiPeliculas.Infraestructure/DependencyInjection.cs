using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TApiPeliculas.Infraestructure.Repository;
using TApiPeliculas.Infraestructure.Repository.Data;
using TApiPeliculas.Infraestructure.Repository.IRepository;
using TApiPeliculas.Infraestructure.Repository.UnitOfWork;

namespace TApiPeliculas.Infraestructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services,
         IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("ConexionSQL"),
                  b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)), ServiceLifetime.Scoped);
            //services.AddScoped<IApplicationDbContext>(provider => provider.GetService<ApplicationDbContext>());
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            return services;
        }
    }
}