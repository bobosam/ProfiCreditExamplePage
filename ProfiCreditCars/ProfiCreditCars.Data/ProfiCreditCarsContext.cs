namespace ProfiCreditCars.Data
{
    using System;
    using System.Data.Entity;

    using ProfiCreditCars.Models;

    /// <summary>
    /// Class create a DbContext ProfiCreditCarsContext.
    /// </summary>
    public class ProfiCreditCarsContext : DbContext
    {
        public ProfiCreditCarsContext()
            : base("ProfiCreditCarsContext")
        {
        }

        public virtual IDbSet<Car> Cars { get; set; }

        /// <summary>
        /// Method Create return new ProfiCreditCarsContext.
        /// </summary>
        /// <returns>new  DbContext</returns>
        public static ProfiCreditCarsContext Create()
        {
            return new ProfiCreditCarsContext();
        }
    }
}