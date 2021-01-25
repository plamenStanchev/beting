namespace OddsSystem.Core.Interfaces.Repository
{
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using OddsSystem.Core.Entities.Base;

    public interface IDeletableEntityRepository<TEntity> : IRepository<TEntity>
        where TEntity : DeletableEntity
    {
        IQueryable<TEntity> AllWithDeleted();

        IQueryable<TEntity> AllAsNoTrackingWithDeleted();

        void HardDelete(TEntity entity);

        void Undelete(TEntity entity);
    }
}
