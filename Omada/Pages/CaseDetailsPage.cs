using Bogus;
using OpenQA.Selenium;

namespace Omada.Pages
{
    public class CaseDetailsPage : OmadaPage
    {
        private IWebDriver _driver;
        private Faker faker;

        private By FirstNameInput => By.CssSelector("[class*='first_name'] input");
        private By LastNameInput => By.CssSelector("[class*='last_name'] input");
        private By JobInput => By.CssSelector("[class*='job_title'] input");
        private By CompanyInput => By.CssSelector("[class*='company'] input");
        private By CountrySelector => By.CssSelector("[class*='country'] select");
        private By CountryInput => By.CssSelector("[class*='email'] input");
        private By EmailInput => By.CssSelector("[class*='email'] input");
        private By PhoneInput => By.CssSelector("[class*='phone'] input");
        private By NrOfEmployersSelector => By.CssSelector("[class*='Employees'] select");
        private By LevelSelector => By.CssSelector("[class*='Level'] select");
        private By AcceptPrivacyRadioBtn => By.CssSelector("input[type = 'radio']");
        private By CaptchaCheckbox => By.CssSelector(".recaptcha-checkbox-checkmark");
        private By SubmitBtn => By.CssSelector("input[type='submit']");

        private By FormIFrame = By.CssSelector("iframe[src*='pardot']");
        private By CaptchaIFrame = By.CssSelector(".g-recaptcha iframe[src*='recaptcha']");



        public CaseDetailsPage(IWebDriver driver) : base(driver)
        {
            _driver = driver;
        }

        public CaseDetailsPage FillFormWithFakeData()
        {
            _driver.SwitchTo().DefaultContent();
            _driver.SwitchTo().Frame(helper.WaitForVisible(FormIFrame));

            faker = new Faker();

            helper.WaitAndSendKeys(FirstNameInput,faker.Name.FirstName());
            helper.WaitAndSendKeys(LastNameInput, faker.Name.LastName());
            helper.WaitAndSendKeys(JobInput, faker.Name.JobTitle());
            helper.WaitAndSendKeys(CompanyInput, faker.Company.CompanyName());
            helper.WaitAndSelectByText(CountrySelector, "Poland");
            helper.WaitAndSendKeys(EmailInput, faker.Internet.Email());
            helper.WaitAndSendKeys(PhoneInput, faker.Phone.PhoneNumber());
            helper.WaitAndSendKeys(NrOfEmployersSelector, "0-500");
            helper.WaitAndSendKeys(LevelSelector, "VP");

            helper.WaitAndClick(AcceptPrivacyRadioBtn);

            _driver.SwitchTo().Frame(helper.WaitForVisible(CaptchaIFrame));

            helper.WaitAndClick(CaptchaCheckbox);

            _driver.SwitchTo().DefaultContent();
            _driver.SwitchTo().Frame(helper.WaitForVisible(FormIFrame));

            helper.GhostClick(SubmitBtn);

            return this;
        }

    }
}
