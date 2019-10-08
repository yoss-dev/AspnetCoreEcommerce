using System;
using System.Linq;
using System.Linq.Expressions;

namespace AspnetCoreEcommerce.Infrastructure.EFRepository
{
    public interface IRepository<TEntity> where TEntity : class
    {
        /// <summary>
        /// Get all Entity
        /// </summary>
        /// <returns>Entity</returns>
        IQueryable<TEntity> GetAll();

        /// <summary>
        /// Finds Entity using linq expression
        /// </summary>
        /// <param name="predicate">linq expression</param>
        /// <returns>Entity</returns>
        TEntity FindByExpression(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// Finds Entities using linq expression
        /// </summary>
        /// <param name="predicate">linq expression</param>
        /// <returns>List of Entities</returns>
        IQueryable<TEntity> FindManyByExpression(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// Find Entity by Id
        /// </summary>
        /// <param name="id">Id of the Entity</param>
        /// <returns>Entity</returns>
        TEntity FindById(Guid id);


        /// <summary>
        /// Insert Entity
        /// </summary>
        /// <param name="entity">Entity</param>
        void Insert(TEntity entity);

        /// <summary>
        /// Updates an Entity
        /// </summary>
        /// <param name="entity">Entity to update</param>
        void Update(TEntity entity);

        /// <summary>
        /// Deletes an Entity
        /// </summary>
        /// <param name="entity">Entity</param>
        void Delete(TEntity entity);


        /// <summary>
        /// Save Changes
        /// </summary>
        void SaveChanges();

    }
}
