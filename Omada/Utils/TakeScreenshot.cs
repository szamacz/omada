using OpenQA.Selenium;

namespace Omada.Utils
{
    public class TakeScreenshot
    {
        private IWebDriver Driver { get; set; }

        public TakeScreenshot(IWebDriver driver)
        {
            Driver = driver;
        }

        public void CurrentWindow(string fileName, string filePath)
        {
            try
            {
                var screenshot = ((ITakesScreenshot)Driver).GetScreenshot();
                screenshot.SaveAsFile($"{filePath}/{fileName}.png", ScreenshotImageFormat.Png);
            }
            catch (System.Exception)
            {

                throw;
            }
        }
    }
}
