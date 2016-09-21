namespace ProfiCreditCars.Data
{
    using ProfiCreditCars.Data.Repositories;
    using ProfiCreditCars.Models;

    /// <summary>
    /// interface that represents our unit of work - it will hold instances of all DbSets
    /// wrapped in repositories and a SaveChanges() method
    /// for committing changes made through those repositories to the database
    /// </summary>
    public interface IProfiCreditCarsData
    {
        IRepository<Car> Cars { get; }

        int SaveChanges();
    }
}
