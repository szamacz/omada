using FluentAssertions;
using Omada.Utils;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;

namespace Omada.Pages
{
    public class SearchPage : OmadaPage
    {
        private IWebDriver _driver;

        private By ResultsList => By.CssSelector(".search-results__item a");
        private By SearchResultHeading => By.CssSelector(".search-results__heading");

        public SearchPage(IWebDriver driver) : base(driver)
        {
            _driver = driver;
        }

        public SearchPage VerifyNumberOfResulstGreaterThanOne()
        {
            var results = GetResults();

            results.Count.Should().BeGreaterThan(1);

            return this;
        }

        private IList<IWebElement> GetResults()
        {
            return helper.WaitForElements(ResultsList);
        }

        public SearchPage OpenArticle(string articleName)
        {
            CloseCookieAndPrivacyPolicy();

            helper.ClickElementWithText(ResultsList, articleName);
            return this;
        }

        public SearchPage VerifyIsAt()
        {
            VerifyPageLoaded();
            helper.WaitForVisible(SearchResultHeading);
            return this;
        }

        public SearchPage VerifyListContainsArticle(string articleName)
        {
            helper.WaitForElementWithText(ResultsList, articleName);
            return this;
        }
    }
}
