using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using So2Baladna.Core.Interfaces;
using So2Baladna.Core.Services;
using So2Baladna.infrastructure.Data;
using So2Baladna.infrastructure.Repositories;
using So2Baladna.Infrastructure.Repositories.Services;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace So2Baladna.infrastructure
{
    public static class InfrastructureRegesteration
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services,IConfiguration configuration)
        {
            // Registering the ApplicationDbContext with the service collection
            
             services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            //  TODO: to avoid this unclean way we can use the following code to register all repositories at once using [UnitOfWork pattern]

            //services.AddScoped<IProductRepository, ProductRepository>();
            //services.AddScoped<IPhotoRepository, PhotoRepository>();
            //services.AddScoped<ICategoryrRepository, CategoryrRepository>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddSingleton<IImageManagementService, ImageManagementService>();
            services.AddSingleton<IFileProvider>(new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot")));
            //apply Redis connection
            services.AddSingleton<IConnectionMultiplexer>(i =>
            {
                var config = ConfigurationOptions.Parse(configuration.GetConnectionString("redis"));
                return ConnectionMultiplexer.Connect(config);
            });

            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });
            services.AddMemoryCache();


            return services;

        }
    }
}
