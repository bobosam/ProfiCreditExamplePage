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
            : this(new ProfiCreditCarsContext())
        {
        }

        public BaseApiController(ProfiCreditCarsContext data)
        {
            this.Data = data;
        }

        public ProfiCreditCarsContext Data { get; set; }
    }
}