namespace ProfiCreditCars.Test.IntegrationTests
{
    using System.Collections.Generic;
    using System.Net;
    using System.Net.Http;
    using System.Web.Http;

    using EntityFramework.Extensions;
    using Microsoft.Owin.Testing;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Owin;

    using ProfiCreditCars.Data;
    using ProfiCreditCars.Services;

    [TestClass]
    public class CarsIntegrationTests
    {
        private static TestServer httpTestServer;
        private static HttpClient httpClient;

        [AssemblyInitialize]
        public static void AssemblyInit(TestContext context)
        {
            httpTestServer = TestServer.Create(appBuilder =>
            {
                var config = new HttpConfiguration();
                WebApiConfig.Register(config);
                appBuilder.UseWebApi(config);
            });

            httpClient = httpTestServer.HttpClient;
            var dbContext = new ProfiCreditCarsContext();
        }

        [TestMethod]
        public void AddCar_WithValidData_ShouldAddCar()
        {
            var context = new ProfiCreditCarsContext();

            var data = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("model", "BMV"),
                new KeyValuePair<string, string>("year", "2012"),
                new KeyValuePair<string, string>("color", "red")
            });

            var response = this.AddNewCar(data);

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [TestMethod]
        public void AddCar_WithShortModelName_ShouldReturnBadRequest()
        {
            var context = new ProfiCreditCarsContext();

            var data = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("model", "B"),
                new KeyValuePair<string, string>("year", "2012"),
                new KeyValuePair<string, string>("color", "red")
            });

            var response = this.AddNewCar(data);

            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [TestMethod]
        public void AddCar_WithLongerModelName_ShouldReturnBadRequest()
        {
            var context = new ProfiCreditCarsContext();

            var data = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("model", "Bbbbbbbbbbbbbbbbbbbbbbbbbbbbbbb"),
                new KeyValuePair<string, string>("year", "2012"),
                new KeyValuePair<string, string>("color", "red")
            });

            var response = this.AddNewCar(data);

            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
        }


        [TestMethod]
        public void AddCar_WithPreviousYear_ShouldReturnBadRequest()
        {
            var context = new ProfiCreditCarsContext();

            var data = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("model", "Opel"),
                new KeyValuePair<string, string>("year", "1700"),
                new KeyValuePair<string, string>("color", "red")
            });

            var response = this.AddNewCar(data);

            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [TestMethod]
        public void AddCar_WithShortColorString_ShouldReturnBadRequest()
        {
            var context = new ProfiCreditCarsContext();

            var data = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("model", "Honda"),
                new KeyValuePair<string, string>("year", "2012"),
                new KeyValuePair<string, string>("color", "r")
            });

            var response = this.AddNewCar(data);

            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [TestMethod]
        public void AddCar_WithLongerColorString_ShouldReturnBadRequest()
        {
            var context = new ProfiCreditCarsContext();

            var data = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("model", "Honda"),
                new KeyValuePair<string, string>("year", "2012"),
                new KeyValuePair<string, string>("color", "Bbbbbbbbbbbbbbbbbbbbbbbbbbbbbbb")
            });

            var response = this.AddNewCar(data);

            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [TestMethod]
        public void GetAllCars_ShouldReturnOk()
        {
            var context = new ProfiCreditCarsContext();
                       
            var response = this.GetAllCar();

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [TestCleanup]
        private static void CleanDatabase()
        {
            var context = new ProfiCreditCarsContext();
            context.Cars.Delete();
        }

        private HttpResponseMessage AddNewCar(FormUrlEncodedContent data)
        {
            return httpClient.PostAsync("/api/cars", data).Result;
        }

        private HttpResponseMessage GetAllCar()
        {
            return httpClient.GetAsync("/api/cars").Result;
        }
    }
}
