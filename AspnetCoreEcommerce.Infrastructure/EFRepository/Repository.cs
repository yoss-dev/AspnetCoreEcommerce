using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace AspnetCoreEcommerce.Infrastructure.EFRepository
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        #region Fields

        private readonly ApplicationDbContext _context;
        private DbSet<TEntity> _entities;

        #endregion

        #region Constructor

        public Repository(ApplicationDbContext context)
        {
            _context = context;
            _entities = context.Set<TEntity>();
        }

        #endregion

        #region Methods

        /// <summary>
        /// Get all Entities
        /// </summary>
        /// <returns>Queryable Collection of Entities</returns>
        public IQueryable<TEntity> GetAll()
        {
            return _entities.AsNoTracking();
        }

        /// <summary>
        /// Finds an Entity using linq expression
        /// </summary>
        /// <param name="predicate">Expression</param>
        /// <returns></returns>
        public TEntity FindByExpression(Expression<Func<TEntity, bool>> predicate)
        {
            return _entities
                .AsNoTracking()
                .SingleOrDefault(predicate);
        }

        public IQueryable<TEntity> FindManyByExpression(Expression<Func<TEntity, bool>> predicate)
        {
            return _entities
                .AsNoTracking()
                .Where(predicate);
        }

        /// <summary>
        /// Find Entity using id
        /// </summary>
        /// <param name="id">Id of the entity</param>
        /// <returns>Entity</returns>
        public TEntity FindById(Guid id)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Inserts Entity
        /// </summary>
        /// <param name="entity"></param>
        public void Insert(TEntity entity)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException("entity");
                _entities.Add(entity);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Update Entity
        /// </summary>
        /// <param name="entity">Entity</param>
        public void Update(TEntity entity)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException("entity");

                _context.Entry(entity).State = EntityState.Modified;
            }
            catch
            {
                throw;
            }
        }

        public void Delete(TEntity entity)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException(nameof(entity));

                _entities.Remove(entity);
            }
            catch
            {
                throw;
            }
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        #endregion
    }
}
