namespace OddsSystem.PublicApi.Extensions
{
    using System.Linq;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.OpenApi.Models;
    using OddsSystem.Core.Interfaces.Infrastructure;
    using OddsSystem.Core.Interfaces.Repository;
    using OddsSystem.Infrastructure.Data;
    using OddsSystem.Infrastructure.Data.Repositories;
    using OddsSystem.Infrastructure.Logging;

    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddDatabase(
            this IServiceCollection services,
            IConfiguration configuration)
        =>
            services
               .AddDbContext<ApplicationDbContext>(options => options
                   .UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

        public static IServiceCollection AddSwagger(this IServiceCollection services)
           => services.AddSwaggerGen(c =>
           {
               c.SwaggerDoc(
                   "v1",
                   new OpenApiInfo
                   {
                       Title = "OddsSystem.Server",
                       Version = "v1",
                   });
               c.ResolveConflictingActions(apiDescription => apiDescription.First());
           });

        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
           => services.AddScoped(typeof(IDeletableEntityRepository<>), typeof(EfDeletableEntityRepository<>))
                      .AddScoped(typeof(IRepository<>), typeof(EfRepository<>))
                      .AddScoped(typeof(IAppLogger<>), typeof(LoggerAdapter<>));
    }
}
