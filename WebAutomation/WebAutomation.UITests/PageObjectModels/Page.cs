using OpenQA.Selenium;
using System;

namespace WebAutomation.UITests.PageObjectModels
{
    class Page
    {
        protected IWebDriver Driver;
      

        public void NavigateTo(string pageUrl)
        {
            Driver.Navigate().GoToUrl(pageUrl);
        }

    }
}
