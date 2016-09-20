namespace ProfiCreditCars.Data
{
    using System;
    using System.Data.Entity;

    using ProfiCreditCars.Models;

    public class ProfiCreditCarsContext : DbContext
    {
        public ProfiCreditCarsContext()
            : base("ProfiCreditCarsContext")
        {
        }

        public virtual IDbSet<Car> Cars { get; set; }
    }
}