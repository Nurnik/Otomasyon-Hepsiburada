using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using System;

namespace WebAutomation.UITests
{
    public sealed class ChromeDriverFixture : IDisposable
    {
        public IWebDriver Driver { get; private set; }

        public ChromeDriverFixture()
        {
            Driver = new ChromeDriver(
ChromeDriverService.CreateDefaultService(".", "chromedriver.exe"));
        }

        public void Dispose()
        {
            Driver.Dispose();
        }
    }
}
