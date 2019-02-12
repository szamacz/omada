using OpenQA.Selenium;
using System.Configuration;

namespace Omada.Pages
{
    public class HomePage : OmadaPage
    {
        private IWebDriver _driver;

        private By SpotsContainer => By.CssSelector(".spots__container");
        private By SearchInput => By.CssSelector(".header__search input");

        public HomePage(IWebDriver driver) : base(driver)
        {
            _driver = driver;
        }

        public HomePage GoTo()
        {
            helper.OpenPage(ConfigurationManager.AppSettings["AppUrl"]);

            return this;
        }

        public HomePage VerifyIsAt()
        {
            VerifyPageLoaded();
            helper.WaitForVisible(SpotsContainer);
            return this;
        }

        public HomePage SearchFor(string searchString)
        {
            helper.WaitAndSendKeys(SearchInput, searchString + Keys.Enter);
            return this;
        }
    }
}
