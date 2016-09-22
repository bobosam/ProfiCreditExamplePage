namespace ProfiCreditCars.Test
{
    using System.Collections.Generic;
    using System.Linq;
    using Moq;

    using ProfiCreditCars.Data.Repositories;
    using ProfiCreditCars.Models;

    public class MockContainer
    {
        public Mock<IRepository<Car>> CarRepositoryMock { get; set; }

        public void PrepareMocks()
        {
            this.SetupFakeCars();
        }

        private void SetupFakeCars()
        {
            var fakeCars = new List<Car>()
            {
                new Car()
                {
                    Id = 4,
                    Model = "BMV",
                    Year = 1999,
                    Color = "red"
                },
                new Car()
                {
                    Id = 7,
                    Model = "Honda",
                    Year = 2010,
                    Color = "green"
                }
            };

            this.CarRepositoryMock = new Mock<IRepository<Car>>();
            this.CarRepositoryMock.Setup(c => c.All())
                .Returns(fakeCars.AsQueryable());
            this.CarRepositoryMock.Setup(c => c.Find(It.IsAny<int>()))
                .Returns((long id) =>
                {
                    return fakeCars.FirstOrDefault(c => c.Id == id);
                });
        }
    }
}
