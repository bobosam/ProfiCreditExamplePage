namespace ProfiCreditCars.Test.UnitTests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Net.Http;
    using System.Threading;
    using System.Web.Http;

    using ProfiCreditCars.Data;
    using ProfiCreditCars.Models;
    using ProfiCreditCars.Services.Controllers;
    using ProfiCreditCars.Services.Models;

    [TestClass]
    public class CarsControllerUnitTests
    {
        private MockContainer mocks;

        [TestInitialize]
        public void InitTest()
        {
            this.mocks = new MockContainer();
            this.mocks.PrepareMocks();
        }

        [TestMethod]
        public void GetAllCars_ShouldReturnAllCars()
        {
            var fakeCars = this.mocks.CarRepositoryMock.Object.All();
            var mockContext = new Mock<IProfiCreditCarsData>();
            mockContext.Setup(m => m.Cars.All()).Returns(fakeCars);
            var carsController = new CarsController(mockContext.Object);
            this.SetupController(carsController);

            var response = carsController.GetCars().ExecuteAsync(CancellationToken.None).Result;

            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
        }

        [TestMethod]
        public void AddCar_WithValithData_ShouldAddNewCar()
        {
            var cars = new List<Car>();

            this.mocks.CarRepositoryMock
                .Setup(c => c.Add(It.IsAny<Car>()))
                .Callback((Car car) =>
                {
                    cars.Add(car);
                });

            var mockContext = new Mock<IProfiCreditCarsData>();
            mockContext.Setup(m => m.Cars).Returns(this.mocks.CarRepositoryMock.Object);

            var carsControler = new CarsController(mockContext.Object);
            this.SetupController(carsControler);

            var randomModel = Guid.NewGuid().ToString();
            var newCar = new CarBindingModel()
            {
                Model = randomModel,
                Year = 2000,
                Color = "red"
            };

            var response = carsControler.PostCar(newCar).ExecuteAsync(CancellationToken.None).Result;

            mockContext.Verify(c => c.SaveChanges(), Times.Once);

            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
            Assert.AreEqual(cars.Count, 1);
            Assert.AreEqual(newCar.Model, cars[0].Model);
        }

        [TestMethod]
        public void DeleteCar_ValidData_SholdDelete()
        {
            var fakeCars = this.mocks.CarRepositoryMock.Object.All();
            var mockContext = new Mock<IProfiCreditCarsData>();
            mockContext.Setup(m => m.Cars.All()).Returns(fakeCars);
            var carsController = new CarsController(mockContext.Object);
            this.SetupController(carsController);

            var response = carsController.DeleteCar(4).ExecuteAsync(CancellationToken.None).Result;

            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
        }

        [TestMethod]
        public void DeleteCar_WrongId_SholdReturnNotFound()
        {
            var fakeCars = this.mocks.CarRepositoryMock.Object.All();
            var mockContext = new Mock<IProfiCreditCarsData>();
            mockContext.Setup(m => m.Cars.All()).Returns(fakeCars);
            var carsController = new CarsController(mockContext.Object);
            this.SetupController(carsController);

            var response = carsController.DeleteCar(1).ExecuteAsync(CancellationToken.None).Result;

            Assert.AreEqual(response.StatusCode, HttpStatusCode.NotFound);
        }

        private void SetupController(CarsController carsController)
        {
            carsController.Request = new HttpRequestMessage();
            carsController.Configuration = new HttpConfiguration();
        }
    }
}
