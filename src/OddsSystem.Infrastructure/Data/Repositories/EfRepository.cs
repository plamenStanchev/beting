namespace OddsSystem.Infrastructure.Data.Repositories
{
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
    using OddsSystem.Core.Entities.Base;
    using OddsSystem.Core.Interfaces.Repository;

    public class EfRepository<TEntity> : IRepository<TEntity>
        where TEntity : Entity
    {
        public EfRepository(ApplicationDbContext context)
        {
            this.Context = context ?? throw new ArgumentNullException(nameof(context));
            this.DbSet = this.Context.Set<TEntity>();
        }

        protected DbSet<TEntity> DbSet { get; set; }

        protected ApplicationDbContext Context { get; set; }

        public virtual IQueryable<TEntity> All() => DbSet;

        public virtual IQueryable<TEntity> AllAsNoTracking() => this.DbSet.AsNoTracking();

        public virtual Task AddAsync(TEntity entity) => this.DbSet.AddAsync(entity).AsTask();

        public virtual void Update(TEntity entity)
        {
            var entry = this.Context.Entry(entity);
            if (entry.State == EntityState.Detached)
            {
                this.DbSet.Attach(entity);
            }

            entry.State = EntityState.Modified;
        }

        public virtual void Delete(TEntity entity) => this.DbSet.Remove(entity);

        public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default) => this.Context.SaveChangesAsync(cancellationToken);

        public void Dispose()
        {
            this.Context?.Dispose();
        }
    }
}
