using Omada.Utils;
using OpenQA.Selenium;
using System.Configuration;

namespace Omada.Pages
{
    public class CaseTakedaDownloadPage : OmadaPage
    {
        private IWebDriver _driver;

        private By DownloadCustomerCase = By.CssSelector(".text__text a");

        public CaseTakedaDownloadPage(IWebDriver driver) : base(driver)
        {
            _driver = driver;
        }

        public CaseTakedaDownloadPage GoTo()
        {
            helper.OpenPage(Constant.TakedaCasePageUrl);
            return this;
        }

        public CaseTakedaDownloadPage DownloadFileAndVerify()
        {
            helper.WaitAndClick(DownloadCustomerCase);
            helper.WaitForFileToDownload(ConfigurationManager.AppSettings["FileDownloadLocation"]
                + Constant.TakedaCaseFileName, Constant.TakedaCaseFileSize);
            return this;
        }
    }
}
