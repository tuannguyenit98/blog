using Common.Exceptions;
using Common.Extentions;
using Common.Runtime.Session;
using Entities.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace EntityFrameworkCore.UnitOfWork
{
    public class EfRepository<TEntity> : IRepository<TEntity> where TEntity : class, IEntity
    {
        public DbContext Context;

        public virtual DbSet<TEntity> Table => Context.Set<TEntity>();

        private int CurrentMaxId;

        public EfRepository(DbContext context)
        {
            Context = context;
            CurrentMaxId = !context.Set<TEntity>().IgnoreQueryFilters().Any() ? 0 : context.Set<TEntity>().IgnoreQueryFilters().Max(x => x.Id);
        }

        public IQueryable<TEntity> GetAll()
        {
            return Table;
        }

        public IQueryable<TEntity> GetAllByCompany()
        {
            if (typeof(ICompanyReferenceEntity).IsAssignableFrom(typeof(TEntity)))
            {
                return Table.Cast<ICompanyReferenceEntity>().Where(x => x.FK_CompanyId == CurrentUser.Current.FK_CompanyId).Cast<TEntity>();
            }
            return Table;
        }

        public IQueryable<TEntity> GetAllIncluding(params Expression<Func<TEntity, object>>[] propertySelectors)
        {
            var query = Table.AsQueryable();

            if (!propertySelectors.IsNullOrEmpty())
            {
                foreach (var propertySelector in propertySelectors)
                {
                    query = query.Include(propertySelector);
                }
            }

            return query;
        }

        public TEntity GetFirstOrDefault(Expression<Func<TEntity, bool>> predicate)
        {
            return GetAll().FirstOrDefault(predicate);
        }

        public Task<TEntity> InsertAsync(TEntity entity)
        {
            return Task.FromResult(Insert(entity));
        }

        public TEntity Insert(TEntity entity)
        {
            //SetId(entity);
            //CheckAndSetDefaultValues(entity);

            Context.Entry(entity).State = EntityState.Added;
            entity.As<IFullEntity>().CreatedAt = DateTime.Now;

            return Table.Add(entity).Entity;
        }

        private void SetId(TEntity entity)
        {
            entity.Id = ++CurrentMaxId;
        }

        public virtual Task DeleteAsync(int id, bool isPhysicalDelete = false)
        {
            Delete(id);
            return Task.FromResult(0);
        }

        public void Delete(int id, bool isPhysicalDelete = false)
        {
            var entity = GetFromChangeTrackerOrNull(id);
            if (entity != null)
            {
                Delete(entity, isPhysicalDelete);
                return;
            }

            entity = Get(id);

            if (entity != null)
            {
                Delete(entity, isPhysicalDelete);
            }

            //Could not found the entity, do nothing.
        }

        private TEntity GetFromChangeTrackerOrNull(int id)
        {
            var entry = Context.ChangeTracker.Entries()
                .FirstOrDefault(
                    ent =>
                        ent.Entity is TEntity &&
                        EqualityComparer<int>.Default.Equals(id, ((TEntity)ent.Entity).Id)
                );

            return entry?.Entity as TEntity;
        }

        public void Delete(TEntity entity, bool isPhysicalDelete = false)
        {
            AttachIfNot(entity);
            if (!isPhysicalDelete)
            {
                CancelDeletionForSoftDelete(entity);
            }
            else
            {
                Table.Remove(entity);
            }
        }

        protected virtual void AttachIfNot(TEntity entity)
        {
            var entry = Context.ChangeTracker.Entries().FirstOrDefault(ent => ent.Entity == entity);
            if (entry != null)
            {
                return;
            }

            Table.Attach(entity);
        }

        public TEntity Get(int id)
        {
            var entity = GetAll().FirstOrDefault(x => x.Id == id);
            if (entity == null)
            {
                throw new EntityNotFoundException(typeof(TEntity), id);
            }

            return entity;
        }

        public Task<TEntity> UpdateAsync(TEntity entity)
        {
            return Task.FromResult(Update(entity));
        }

        public TEntity Update(TEntity entity)
        {
            AttachIfNot(entity);
            Context.Entry(entity).State = EntityState.Modified;
            entity.As<IFullEntity>().UpdatedAt = DateTime.Now;
            return entity;
        }

        public Task<int> CountAsync()
        {
            return Table.CountAsync();
        }

        private void CancelDeletionForSoftDelete(TEntity entity)
        {
            if (entity is not IFullEntity)
            {
                return;
            }

            Context.Entry(entity).State = EntityState.Modified;
            entity.As<IFullEntity>().DeleteAt = DateTime.Now;
        }
    }
}
