namespace ProfiCreditCars.Data.Repositories
{
    using System.Data.Entity;
    using System.Linq;

    /// <summary>
    /// generic class for creating repositories of custom type T, 
    /// which implements the IRepository interface.
    /// </summary>
    /// <typeparam name="T">generic type</typeparam>
    public class GenericRepository<T> : IRepository<T> where T : class
    {
        protected DbContext context;
        protected DbSet<T> set;

        public GenericRepository(DbContext context)
        {
            this.context = context;
            this.set = context.Set<T>();
        }

        /// <summary>
        /// Add entity to repository
        /// </summary>
        /// <param name="entity">the entity model to be added</param>
        public void Add(T entity)
        {
            this.ChangeState(entity, EntityState.Added);
        }

        /// <summary>
        /// Find all entities.
        /// </summary>
        /// <returns>IQuerable collection from entities</returns>
        public IQueryable<T> All()
        {
            return this.set.AsQueryable();
        }

        /// <summary>
        /// Delete specific entity
        /// </summary>
        /// <param name="entity">parameter specific entity</param>
        public void Delete(T entity)
        {
            this.ChangeState(entity, EntityState.Deleted);
        }

        /// <summary>
        /// method finds a specific entity 
        /// </summary>
        /// <param name="id">parameter by which the method finds a specific entity</param>
        /// <returns>current entity</returns>
        public T Find(long id)
        {
            return this.set.Find(id);
        }

        private void ChangeState(T entity, EntityState state)
        {
            var entry = this.context.Entry(entity);
            if (entry.State == EntityState.Detached)
            {
                this.set.Attach(entity);
            }

            entry.State = state;
        }
    }
}
