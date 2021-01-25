namespace OddsSystem.Core.Interfaces.Repository
{
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using OddsSystem.Core.Entities.Base;

    public interface IRepository<TEntity> : IDisposable
        where TEntity : Entity
    {
        IQueryable<TEntity> All();

        IQueryable<TEntity> AllAsNoTracking();

        Task AddAsync(TEntity entity);

        void Update(TEntity entity);

        void Delete(TEntity entity);

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
