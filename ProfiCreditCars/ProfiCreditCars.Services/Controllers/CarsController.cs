namespace ProfiCreditCars.Services.Controllers
{
    using System.Data;
    using System.Linq;
    using System.Web.Http;
    using System.Web.Http.Description;

    using ProfiCreditCars.Models;
    using ProfiCreditCars.Services.Models;

    /// <summary>
    /// A RestController class where we define the endpoint URLs for the Car service. 
    /// </summary>
    public class CarsController : BaseApiController
    {
        /// <summary>
        /// GET Serving the method
        /// </summary>
        /// <returns>a json string array of all cars or empty array</returns>
        /// GET: api/cars
        [HttpGet]
        public IHttpActionResult GetCars()
        {
            var data = this.Data.Cars.All()
                .Select(CarViewModel.Create);

            return this.Ok(data);
        }

        /// <summary>
        /// GET Serving the method by id
        /// </summary>
        /// <param name="id">a path parameter by which the method finds a specific car</param>
        /// <returns>a json representation of a car or empty string</returns>
        /// GET: api/cars/5
        [ResponseType(typeof(Car))]
        public IHttpActionResult GetCar(long id)
        {
            var data = this.Data.Cars.All()
                .Where(c => c.Id == id)
                .Select(CarViewModel.Create)
                .FirstOrDefault();

            if (data == null)
            {
                return this.NotFound();
            }

            return this.Ok(data);
        }

        /// <summary>
        /// POST Serves the method. Saves a car in the database.
        /// </summary>
        /// <param name="model">the car model to be saved which is passed by a json body request parameter</param>
        /// <returns>json string of car view model</returns>
        /// POST: api/cars
        [ResponseType(typeof(Car))]
        public IHttpActionResult PostCar(CarBindingModel model)
        {
            if (model == null)
            {
                return this.BadRequest("Model cannot be null.");
            }

            if (!ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            var car = new Car()
            {
                Model = model.Model,
                Year = model.Year,
                Color = model.Color
            };

            this.Data.Cars.Add(car);
            this.Data.SaveChanges();

            var data = this.Data.Cars.All()
                .Where(c => c.Id == car.Id)
                .Select(CarViewModel.Create)
                .FirstOrDefault();

            return this.Ok(data);
        }

        /// <summary>
        /// DELETE Serving the method.
        /// </summary>
        /// <param name="id">a path parameter by which the method finds a specific car</param>
        /// <returns>json string of car</returns>
        /// DELETE: api/cars/5
        [ResponseType(typeof(Car))]
        public IHttpActionResult DeleteCar(long id)
        {
            Car car = this.Data.Cars.Find(id);
            if (car == null)
            {
                return this.NotFound();
            }

            this.Data.Cars.Delete(car);
            this.Data.SaveChanges();

            return this.Ok(car);
        }
    }
}