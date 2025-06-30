using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using So2Baladna.Core.Interfaces;
using So2Baladna.infrastructure.Data;
using So2Baladna.infrastructure.Repositories;
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
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });
            return services;

        }
    }
}
