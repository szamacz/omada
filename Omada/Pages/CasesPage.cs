using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace Omada.Pages
{
    public class CasesPage : OmadaPage
    {
        private IWebDriver _driver;

        private By DownloadPDFButtonList => By.CssSelector(".cases__button.button--variant2");

        public CasesPage(IWebDriver driver) : base(driver)
        {
            _driver = driver;
        }

        public CasesPage OpenTakedDownloadPage()
        {
            var firstResult = helper.WaitAndClick(DownloadPDFButtonList);
            return this;
        }
    }
}
