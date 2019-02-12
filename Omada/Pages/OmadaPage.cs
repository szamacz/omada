using Omada.Utils;
using OpenQA.Selenium;

namespace Omada.Pages
{
    public abstract class OmadaPage
    {
        protected Helper helper;

        protected OmadaPage(IWebDriver driver)
        {
            helper = new Helper(driver);
        }

        private By Navigation => By.CssSelector("[data-init='top-navigation']");
        private By PageFooter => By.CssSelector(".brick.footer--variant4");
        private By ClosePrivacyPolicyBtn => By.CssSelector(".cookiebar__button-container span");
        private By MainMenu => By.CssSelector(".header__menulink--megamenu.js-menulink");
        private By NewsSubsection => By.CssSelector(".header__menuitem--submenu [href*='/more/news-events/news']");
        private By ContactBtn => By.CssSelector(".header__menu--function-nav [href*='/more/company/contact']");
        private By ReadPrivacyPolicyBtn => By.CssSelector(".cookiebar__read-more");
        private By OpenCasesBtn => By.CssSelector(".footer__menuitem--submenu [href*='more/customers/cases']");

        public void VerifyPageLoaded()
        {
            helper.WaitForVisible(Navigation);
            helper.WaitForVisible(PageFooter);
        }

        public void GoToNewsSubsection()
        {
            helper.MoveToElementWithText(MainMenu, Constant.MoreSectionName);
            helper.WaitAndClick(NewsSubsection);
        }

        public void GoToContactPage()
        {
            helper.WaitAndClick(ContactBtn);
        }

        public string OpenReadPrivacyPolicyInNewTab()
        {
            var currentWindowHadle = helper.GetCurrentWindowHandle();
            var url = helper.WaitForClickable(ReadPrivacyPolicyBtn).GetAttribute("href");
            helper.OpenNewTab();
            helper.OpenPage(url);
            return currentWindowHadle;
        }

        public void VerifyPrivacyPolicyNotVisible()
        {
            helper.WaitForInvisible(ReadPrivacyPolicyBtn);
        }

        public void CloseCookieAndPrivacyPolicy()
        {

            var closeBtn = helper.WaitForElement(ClosePrivacyPolicyBtn);
            if (closeBtn.Displayed)
                helper.WaitAndClick(ClosePrivacyPolicyBtn);
        }

        public void GoToCasesPage()
        {
            CloseCookieAndPrivacyPolicy();
            helper.WaitAndClick(OpenCasesBtn);
        }
    }
}
