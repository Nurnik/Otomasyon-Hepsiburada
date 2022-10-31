using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using WebAutomation.UITests.PageObjectModels;
using OpenQA.Selenium;
using Xunit;
using Xunit.Abstractions;

namespace WebAutomation.UITests
{
    public class WebAutomationApplicationShould : IClassFixture<ChromeDriverFixture>
    {                
        private readonly ChromeDriverFixture ChromeDriverFixture;
        private readonly ITestOutputHelper output;

        public WebAutomationApplicationShould(ChromeDriverFixture chromeDriverFixture, ITestOutputHelper output)
        {
            ChromeDriverFixture = chromeDriverFixture;

            //Çerezler silinir
            ChromeDriverFixture.Driver.Manage().Cookies.DeleteAllCookies();
            ChromeDriverFixture.Driver.Navigate().GoToUrl("about:blank");

            this.output = output;


        }

        [Fact]
        public void BeInitiatedFromSearchPage()
        {
            List<SearchResult> chromeResult = new List<SearchResult>();
            List<SearchResult> yandexResult = new List<SearchResult>();
            List<SearchResult> resultUnion = new List<SearchResult>();
            var chromeSearchPage = new SearchPage(ChromeDriverFixture.Driver);

            chromeSearchPage.NavigateTo("https://www.google.com/search?q=tarkan&num=10");
            chromeResult = DemoHelper.GetGoogleSearchResults(ChromeDriverFixture);


            chromeSearchPage.NavigateTo("https://yandex.com.tr/search/?text=tarkan");
            yandexResult = DemoHelper.GetYandexSearchResults(ChromeDriverFixture);
            ChromeDriverFixture.Driver.Quit();
            resultUnion = DemoHelper.GetUnionResultofSearchEngines(chromeResult, yandexResult);

            output.WriteLine("{0} sonuç google ve yandex aramasında da bulundu.",resultUnion.Count);
            foreach (var item in resultUnion)
            {
                output.WriteLine("Title:"+item.Title);
                output.WriteLine("URL:" + item.Url);
                output.WriteLine("Description:" + item.Description);
            }

        }


    }
}
