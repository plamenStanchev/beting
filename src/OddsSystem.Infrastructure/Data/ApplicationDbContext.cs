namespace OddsSystem.Infrastructure.Data
{
    using System.Reflection;
    using Microsoft.EntityFrameworkCore;
    using OddsSystem.Core.Entities.Base;
    using OddsSystem.Core.Entities.Models;

    public class ApplicationDbContext : DbContext
    {
        private static readonly MethodInfo SetIsDeletedQueryFilterMethod =
            typeof(ApplicationDbContext).GetMethod(
                nameof(SetIsDeletedQueryFilter),
                BindingFlags.NonPublic | BindingFlags.Static);

        public ApplicationDbContext(DbContextOptions options)
            : base(options)
        {
        }

        public DbSet<Event> Events { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(this.GetType().Assembly);
        }

        private static void SetIsDeletedQueryFilter<T>(ModelBuilder builder)
           where T : DeletableEntity
        {
            builder.Entity<T>().HasQueryFilter(e => !e.IsDeleted);
        }
    }
}
