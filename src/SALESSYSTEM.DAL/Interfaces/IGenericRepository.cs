using System.Linq.Expressions;

namespace SALESSYSTEM.DAL.Interfaces
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        Task<TEntity> Get(Expression<Func<TEntity, bool>> filter);

        Task<TEntity> Create(TEntity entity);
        Task<bool> Update(TEntity entity);
        Task<bool> Remove(TEntity entity);
        Task<IQueryable<TEntity>> GetEntityQuery(Expression<Func<TEntity, bool>>? filter =null);
    }
}
