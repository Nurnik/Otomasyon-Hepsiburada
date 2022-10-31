using WebAutomation.UITests.PageObjectModels;
using OpenQA.Selenium;
using System.Collections.Generic;
using System;
using System.Threading;
using System.Linq;

namespace WebAutomation.UITests
{
    internal static class DemoHelper
    {
        ///// <summary>
        ///// Brief delay to slow down browser interactions for
        ///// demo video recording purposes
        ///// </summary>
        public static void Pause(int secondsToPause = 3000)
        {
            Thread.Sleep(secondsToPause);
        }
        public static List<SearchResult> GetGoogleSearchResults(ChromeDriverFixture chromeDriverFixture)
        {
            int count = 0;
            var pages = chromeDriverFixture.Driver.FindElements(By.ClassName("tF2Cxc"));//LC20lb MBeuO DKV0Md //tF2Cxc
            List<SearchResult> chromeResult = new List<SearchResult>();

            foreach (var page in pages)
            {
                if (!String.IsNullOrWhiteSpace(page.Text))
                {
                    count++;
                    if (count == 11)
                        break;
                    string result = page.Text;
                    result = result.Replace("\r\n", "+");
                    var results = result.Split('+');
                    results[1] = results[1].Replace(" › ", "/");
                    string itemUrl = page.FindElement(By.TagName("a")).GetAttribute("href");
                    SearchResult resultItem = new SearchResult()
                    {
                        Title = results[0],
                        Url = itemUrl,
                        Description = results[2]
                    };
                    chromeResult.Add(resultItem);
                }
            }
            return chromeResult;
        }
        public static List<SearchResult> GetYandexSearchResults(ChromeDriverFixture chromeDriverFixture)
        {
            int count = 0;
            var pages = chromeDriverFixture.Driver.FindElements(By.ClassName("serp-item_card"));//LC20lb MBeuO DKV0Md //tF2Cxc
            List<SearchResult> yandexResult = new List<SearchResult>();

            foreach (var page in pages)
            {
                try
                {
                    if (!String.IsNullOrWhiteSpace(page.Text))
                    {
                        count++;
                        if (count == 11)
                            break;
                        string result = page.Text;
                        result = result.Replace("\r\n", "+");
                        var results = result.Split('+');
                        results[1] = results[1].Replace(" › ", "/");
                        string title = page.FindElement(By.ClassName("OrganicTitleContentSpan")).Text;

                        string itemUrl = page.FindElement(By.TagName("a")).GetAttribute("href");
                        string description = page.FindElement(By.ClassName("OrganicTextContentSpan")).Text;

                        SearchResult resultItem = new SearchResult()
                        {
                            Title = title,
                            Url = itemUrl,
                            Description = description
                        };
                        yandexResult.Add(resultItem);
                    }
                }
                catch (Exception)
                {

                    continue;
                }
               
            }
            return yandexResult;
        }

        internal static List<SearchResult> GetUnionResultofSearchEngines(List<SearchResult> chromeResult, List<SearchResult> yandexResult)
        {
            List<SearchResult> resultUnion = new List<SearchResult>();
           
            for (int i = 0; i < 10; i++)
            {
                try
                {
                    if (yandexResult.Any(n => n.Url == chromeResult[i].Url))
                    {
                        resultUnion.Add(chromeResult[i]);
                    }
                }
                catch (Exception)
                {

                    continue;
                }
              
            }
            return resultUnion;
        }
    }
}
