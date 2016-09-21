namespace ProfiCreditCars.Data.Repositories
{
    using System.Linq;

    /// <summary>
    /// Generic repository interface for create abstraction layer
    /// between controller and DbContext.
    /// </summary>
    /// <typeparam name="T">generic type</typeparam>
    public interface IRepository<T>
    {
        /// <summary>
        /// Find all entities.
        /// </summary>
        /// <returns>IQuerable collection from entities</returns>
        IQueryable<T> All();

        /// <summary>
        /// method finds a specific entity 
        /// </summary>
        /// <param name="id">parameter by which the method finds a specific entity</param>
        /// <returns>current entity</returns>
        T Find(long id);

        /// <summary>
        /// Add entity to repository
        /// </summary>
        /// <param name="entity">the entity model to be added</param>
        void Add(T entity);

        /// <summary>
        /// Delete specific entity
        /// </summary>
        /// <param name="entity">parameter specific entity</param>
        void Delete(T entity);
    }
}
