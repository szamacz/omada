using NUnit.Framework;
using Omada.Pages;
using Omada.Utils;
using OpenQA.Selenium;
using Serilog;
using System.Configuration;

namespace Omada.Tests
{
    public class BaseTest
    {
        private IWebDriver _driver;
        private BrowserManager _browserManager;

        protected HomePage homePage;
        protected SearchPage searchPage;
        protected NewsPage newsPage;
        protected NewsDetailsPage newsDetailsPage;
        protected ContactPage contactPage;
        protected PrivacyStatementPage privacyStatementPage;
        protected CasesPage casesPage;
        protected CaseDetailsPage caseDetailsPage;
        protected CaseTakedaDownloadPage caseTakedaDownloadPage;

        [OneTimeSetUp]
        public void InitPages()
        {
            _browserManager = new BrowserManager();
            _driver = _browserManager.GetDriver(ConfigurationManager.AppSettings["Browser"]);
            LoggerHelper.SetupLogger();

            homePage = new HomePage(_driver);
            searchPage = new SearchPage(_driver);
            newsPage = new NewsPage(_driver);
            newsDetailsPage = new NewsDetailsPage(_driver);
            contactPage = new ContactPage(_driver);
            privacyStatementPage = new PrivacyStatementPage(_driver);
            casesPage = new CasesPage(_driver);
            caseDetailsPage = new CaseDetailsPage(_driver);
            caseTakedaDownloadPage = new CaseTakedaDownloadPage(_driver);
        }

        [OneTimeTearDown]
        public void Cleanup()
        {
            _driver.Quit();
        }

        [SetUp]
        public void Before()
        {
            Log.Information($"------------------ Starting test {TestContext.CurrentContext.Test.Name} -------------------");
        }

        [TearDown]
        public void After()
        {
            Log.Information($"------------------ Finishing test {TestContext.CurrentContext.Test.Name} ------------------");
        }

        protected void RefreshPage()
        {
            _driver.Navigate().Refresh();
        }
    }
}
