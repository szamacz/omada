using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using System;
using System.Configuration;

namespace Omada.Utils
{
    public class BrowserManager
    {
        private IWebDriver _driver;

        private string _downloadPath = ConfigurationManager.AppSettings["FileDownloadLocation"];

        public IWebDriver GetDriver(string browser)
        {
            if (_driver != null)
            {
                return _driver;
            }
            switch (browser)
            {
                case "Chrome":
                    {
                        var chromeOptions = new ChromeOptions();
                        chromeOptions.AddUserProfilePreference("download.default_directory", _downloadPath);
                        chromeOptions.AddUserProfilePreference("disable-popup-blocking", "true");
                        _driver = new ChromeDriver(chromeOptions);
                        break;
                    }
                case "Firefox":
                    {
                        var options = new FirefoxOptions();
                        var profile = new FirefoxProfile();
                        profile.SetPreference("browser.download.folderList", 2);
                        profile.SetPreference("browser.download.dir", _downloadPath);
                        profile.SetPreference("browser.download.useDownloadDir", true);
                        profile.SetPreference("browser.helperApps.neverAsk.saveToDisk", "application/octet-stream doc xls pdf txt");
                        profile.SetPreference("pdfjs.disabled", true);
                        options.Profile = profile;
                        _driver = new FirefoxDriver(options);
                        break;
                    }
                default:
                    throw new NotImplementedException("Handling for specified browser not implemented");
            }
            _driver.Manage().Window.Maximize();
            _driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(int.Parse(ConfigurationManager.AppSettings["PageLoadTimeout"]));
            return _driver;
        }
    }
}
