namespace ProfiCreditCars.Test.SeleniumAutomationTests
{
    using OpenQA.Selenium;
    using OpenQA.Selenium.Support.PageObjects;

    public class CarPage
    {
        private IWebDriver driver;

        public CarPage(IWebDriver driver)
        {
            this.driver = driver;
            PageFactory.InitElements(driver, this);
        }

        [FindsBy(How = How.Id, Using = "model")]
        public IWebElement Model { get; set; }

        [FindsBy(How = How.Id, Using = "year")]
        public IWebElement Year { get; set; }

        [FindsBy(How = How.Id, Using = "color")]
        public IWebElement Color { get; set; }

        [FindsBy(How = How.Id, Using = "add")]
        public IWebElement AddButton { get; set; }

        [FindsBy(How = How.Id, Using = "pre")]
        public IWebElement Pre { get; set; }
    }
}
