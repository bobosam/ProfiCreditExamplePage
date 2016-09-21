namespace ProfiCreditCars.Services.Models
{
    using System;
    using System.Linq.Expressions;

    using ProfiCreditCars.Models;

    /// <summary>
    /// Class contains all properties to view car.
    /// </summary>
    public class CarViewModel
    {
        /// <summary>
        /// Gets this expression create car view model.
        /// </summary>
        public static Expression<Func<Car, CarViewModel>> Create
        {
            get
            {
                return car => new CarViewModel
                {
                    Id = car.Id,
                    Model = car.Model,
                    Year = car.Year,
                    Color = car.Color
                };
            }
        }

        public long Id { get; set; }

        public string Model { get; set; }

        public int Year { get; set; }

        public string Color { get; set; }
    }
}