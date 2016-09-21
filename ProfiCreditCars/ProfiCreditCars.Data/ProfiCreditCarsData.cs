namespace ProfiCreditCars.Data
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;

    using ProfiCreditCars.Data.Repositories;
    using ProfiCreditCars.Models;

    /// <summary>
    /// class implement a concrete unit of work
    /// </summary>
    public class ProfiCreditCarsData : IProfiCreditCarsData
    {
        private DbContext context;
        private IDictionary<Type, object> repositories;

        public ProfiCreditCarsData(DbContext context)
        {
            this.context = context;
            this.repositories = new Dictionary<Type, object>();
        }

        public IRepository<Car> Cars
        {
            get
            {
                return this.GetRepository<Car>();
            }
        }

        public int SaveChanges()
        {
            return this.context.SaveChanges();
        }

        private IRepository<T> GetRepository<T>() where T : class
        {
            var type = typeof(T);
            if (!this.repositories.ContainsKey(type))
            {
                var typeOfRepository = typeof(GenericRepository<T>);
                var repository = Activator.CreateInstance(
                    typeOfRepository, this.context);

                this.repositories.Add(type, repository);
            }

            return (IRepository<T>)this.repositories[type];
        }
    }
}
