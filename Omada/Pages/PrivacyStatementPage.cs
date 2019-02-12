using Omada.Utils;
using OpenQA.Selenium;

namespace Omada.Pages
{
    public class PrivacyStatementPage : OmadaPage
    {
        private IWebDriver _driver;

        public PrivacyStatementPage(IWebDriver driver) : base(driver)
        {
            _driver = driver;
        }

        private By TextHeader => By.CssSelector(".text__heading");

        public PrivacyStatementPage VerifyIsAt()
        {
            VerifyPageLoaded();
            helper.WaitForText(TextHeader, Constant.PrivacyPolicyHeader);  
            return this;
        }

        public PrivacyStatementPage CloseTabAndSwitchBackTo(string handle)
        {
            helper.CloseTab();
            _driver.SwitchTo().Window(handle);
            return this;
        }
    }
}
