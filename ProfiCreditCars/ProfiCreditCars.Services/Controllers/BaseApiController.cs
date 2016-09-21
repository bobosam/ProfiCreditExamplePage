namespace ProfiCreditCars.Services.Controllers
{
    using System.Web.Http;

    using ProfiCreditCars.Data;

    /// <summary>
    /// A base controller where we create new DbContext ProfiCreditCarsContext. 
    /// </summary>
    public class BaseApiController : ApiController
    {
        public BaseApiController()
            : this(new ProfiCreditCarsData(new ProfiCreditCarsContext()))
        {
        }

        public BaseApiController(IProfiCreditCarsData data)
        {
            this.Data = data;
        }

        public IProfiCreditCarsData Data { get; set; }
    }
}