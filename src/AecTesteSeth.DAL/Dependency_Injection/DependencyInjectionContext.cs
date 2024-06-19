using AeCTesteSeth.DAL.Context;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using AeCTesteSeth.DOMAIN;
using AeCTesteSeth.DOMAIN.DAL.Interfaces;
using AeCTesteSeth.DAL.Repositorios;

namespace AeCTesteSeth.DAL.Dependency_Injection
{
    public static class DependencyInjectionContext
    {
        public static IServiceCollection AddDALServices(this IServiceCollection services,
           IConfiguration configuration)
        {
            services.AddScoped<IUsuarioRepository, UsuarioRepository>();
            services.AddScoped<IEnderecoRepository, EnderecoRepository>();
            //services.AddTransient<IUsuarioRepository, UsuarioRepository>();
            //services.AddTransient<IEnderecoRepository, EnderecoRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            //services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddDbContext<MyContext>(opt => { opt
                .UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
                opt.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking); 
            });              

            return services;
        }
    }
}
