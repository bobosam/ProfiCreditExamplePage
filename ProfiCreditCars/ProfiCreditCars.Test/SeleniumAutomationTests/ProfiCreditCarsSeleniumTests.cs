namespace ProfiCreditCars.Test.SeleniumAutomationTests
{
    using System;
    using System.Threading;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using OpenQA.Selenium;
    using OpenQA.Selenium.Chrome;
      
    [TestClass]
    public class ProfiCreditCarsSeleniumTests
    {
        private const string UrlString = "http://localhost:63342/carsjsFrontend/index.html";
        private IWebDriver driver;

        [TestInitialize]
        public void Setup()
        {
            this.driver = new ChromeDriver();

            this.driver.Navigate().GoToUrl(UrlString);
            this.driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(5));
            this.driver.Manage().Window.Maximize();
        }

        [TestMethod]
        public void AddCar_ValidData_ShouldBeAdd()
        {
            CarPage carPage = new CarPage(this.driver);

            carPage.Model.SendKeys("BMV");
            carPage.Year.SendKeys("2010");
            carPage.Color.SendKeys("red");
            carPage.AddButton.Click();

            Thread.Sleep(2000);

            var message = carPage.Pre.GetAttribute("textContent");

            Assert.AreEqual(message, "success");
        }

        [TestMethod]
        public void AddCar_JSModelInjection_ShouldBeAddWithSameModelName()
        {
            CarPage carPage = new CarPage(this.driver);

            carPage.Model.SendKeys("<script>alert(1)</script>");
            carPage.Year.SendKeys("2010");
            carPage.Color.SendKeys("red");
            carPage.AddButton.Click();

            Thread.Sleep(2000);

            var message = carPage.Pre.GetAttribute("textContent");

            Assert.AreEqual(message, "success");
        }

        [TestMethod]
        public void AddCar_ShortModelName_ShouldBeErrorMessage()
        {
            CarPage carPage = new CarPage(this.driver);

            carPage.Model.SendKeys("B");
            carPage.Year.SendKeys("2010");
            carPage.Color.SendKeys("red");
            carPage.AddButton.Click();

            Thread.Sleep(2000);

            var message = carPage.Pre.GetAttribute("textContent");

            Assert.AreEqual(message, "error");
        }

        [TestMethod]
        public void AddCar_LongerModelName_ShouldBeErrorMessage()
        {
            CarPage carPage = new CarPage(this.driver);

            carPage.Model.SendKeys("Bbbbbbbbbbbbbbbbbbbbbbbbbb");
            carPage.Year.SendKeys("2010");
            carPage.Color.SendKeys("red");
            carPage.AddButton.Click();

            Thread.Sleep(2000);

            var message = carPage.Pre.GetAttribute("textContent");

            Assert.AreEqual(message, "error");
        }

        [TestMethod]
        public void AddCar_JSYearlInjection_ShouldBeErrorMessaget()
        {
            CarPage carPage = new CarPage(this.driver);

            carPage.Model.SendKeys("Honda");
            carPage.Year.SendKeys("<script>alert(1)</script>");
            carPage.Color.SendKeys("red");
            carPage.AddButton.Click();

            Thread.Sleep(2000);

            var message = carPage.Pre.GetAttribute("textContent");

            Assert.AreEqual(message, "error");
        }

        [TestMethod]
        public void AddCar_EarlierYear_ShouldBeErrorMessaget()
        {
            CarPage carPage = new CarPage(this.driver);

            carPage.Model.SendKeys("Honda");
            carPage.Year.SendKeys("1767");
            carPage.Color.SendKeys("red");
            carPage.AddButton.Click();

            Thread.Sleep(2000);

            var message = carPage.Pre.GetAttribute("textContent");

            Assert.AreEqual(message, "error");
        }

        [TestMethod]
        public void AddCar_JSColorInjection_ShouldBeAddWithSameColorName()
        {
            CarPage carPage = new CarPage(this.driver);

            carPage.Model.SendKeys("Opel");
            carPage.Year.SendKeys("2010");
            carPage.Color.SendKeys("<script>alert(1)</script>");
            carPage.AddButton.Click();

            Thread.Sleep(2000);

            var message = carPage.Pre.GetAttribute("textContent");

            Assert.AreEqual(message, "success");
        }

        [TestMethod]
        public void AddCar_ShortColor_ShouldBeErrorMessage()
        {
            CarPage carPage = new CarPage(this.driver);

            carPage.Model.SendKeys("Opel");
            carPage.Year.SendKeys("2010");
            carPage.Color.SendKeys("r");
            carPage.AddButton.Click();

            Thread.Sleep(2000);

            var message = carPage.Pre.GetAttribute("textContent");

            Assert.AreEqual(message, "error");
        }

        [TestMethod]
        public void AddCar_LongerColor_ShouldBeErrorMessage()
        {
            CarPage carPage = new CarPage(this.driver);

            carPage.Model.SendKeys("Opel");
            carPage.Year.SendKeys("2010");
            carPage.Color.SendKeys("Bbbbbbbbbbbbbbbbbbbbbbbbbb");
            carPage.AddButton.Click();

            Thread.Sleep(2000);

            var message = carPage.Pre.GetAttribute("textContent");

            Assert.AreEqual(message, "error");
        }

        [TestCleanup]
        public void Close()
        {
            this.driver.Close();
        }
    }
}
