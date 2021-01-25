namespace OddsSystem.PublicApi.Extensions
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using OddsSystem.Infrastructure.Data;

    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseSwaggerUI(this IApplicationBuilder app)
           => app
               .UseSwagger()
               .UseSwaggerUI(options =>
               {
                   options.SwaggerEndpoint("/swagger/v1/swagger.json", "OddsSystem");
                   options.RoutePrefix = string.Empty;
               });

        public static void ApplyMigrations(this IApplicationBuilder app)
        {
            using var services = app.ApplicationServices.CreateScope();

            var dbContext = services.ServiceProvider.GetService<ApplicationDbContext>();

            dbContext.Database.Migrate();
        }

        public static void UseCorsConfigured(this IApplicationBuilder app)
            => app.UseCors(options =>
            {
                options.AllowAnyOrigin();
                options.AllowAnyMethod();
                options.AllowAnyHeader();
            });
    }
}
