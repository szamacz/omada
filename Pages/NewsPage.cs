using Omada.Utils;
using OpenQA.Selenium;

namespace Omada.Pages
{
    public class NewsPage : OmadaPage
    {
        private IWebDriver _driver;

        private By NewsList => By.CssSelector(".cases__heading");
        

        public NewsPage(IWebDriver driver) : base(driver)
        {
            _driver = driver;
        }

        public NewsPage VerifyNewsContainsArticle(string articleName)
        {
            helper.WaitForElementWithText(NewsList, articleName);
            return this;
        }
    }
}
