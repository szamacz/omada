using NUnit.Framework;
using Omada.Utils;
using OpenQA.Selenium;
using System.Configuration;

namespace Omada.Pages
{
    public class ContactPage : OmadaPage
    {
        private IWebDriver _driver;
        private TakeScreenshot _takeScreenshot;

        private By LocationMenuItemList => By.CssSelector(".tabmenu__menu-item");
        private By LocationMenuSelectedItem => By.CssSelector(".tabmenu__menu-item.selected");

        public ContactPage(IWebDriver driver) : base(driver)
        {
            _driver = driver;
            _takeScreenshot = new TakeScreenshot(driver);
        }

        public ContactPage SelectLocation(string locationName)
        {
            helper.WaitForText(LocationMenuSelectedItem, "Denmark");
            helper.ClickElementWithText(LocationMenuItemList, locationName);
            helper.WaitForText(LocationMenuSelectedItem, locationName);
            TakeScreenshot();
            return this;
        }

        public ContactPage HoverOverLocation(string locationName)
        {
            helper.MoveToElementWithText(LocationMenuItemList, locationName);
            TakeScreenshot();
            return this;
        }

        private void TakeScreenshot()
        {
            _takeScreenshot.CurrentWindow(TestContext.CurrentContext.Test.Name + "_" + helper.GetCurentTimespanAsString(),
               ConfigurationManager.AppSettings["ScreenshotLocation"]);
        }
    }
}
