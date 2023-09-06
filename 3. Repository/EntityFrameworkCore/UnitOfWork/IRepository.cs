using Entities.Interfaces;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace EntityFrameworkCore.UnitOfWork
{
    public interface IRepository<TEntity> where TEntity : class, IEntity
    {
        IQueryable<TEntity> GetAll();

        IQueryable<TEntity> GetAllByCompany();

        IQueryable<TEntity> GetAllIncluding(params Expression<Func<TEntity, object>>[] propertySelectors);

        TEntity GetFirstOrDefault(Expression<Func<TEntity, bool>> predicate);

        Task<TEntity> InsertAsync(TEntity entity);

        Task DeleteAsync(int id, bool isPhysicalDelete = false);

        Task<TEntity> UpdateAsync(TEntity entity);
        
        Task<int> CountAsync();
    }
}
