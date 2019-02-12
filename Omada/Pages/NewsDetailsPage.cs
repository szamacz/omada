using FluentAssertions;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Omada.Pages
{
    public class NewsDetailsPage : OmadaPage
    {
        private IWebDriver _driver;

        private By NewsTitle => By.CssSelector(".text__heading");
        private By NewsBody => By.CssSelector(".brick.text--variant0  .text__text");


        public NewsDetailsPage(IWebDriver driver) : base(driver)
        {
            _driver = driver;
        }

        public NewsDetailsPage VerifyNewsArticle(string newsTitle)
        {
            helper.WaitForVisible(NewsTitle).Text.Should().Be(newsTitle);
            helper.WaitForVisible(NewsBody);

            return this;
        }
    }
}
