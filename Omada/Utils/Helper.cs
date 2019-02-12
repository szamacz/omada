using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using FluentAssertions;
using System.Configuration;
using Serilog;
using System.Diagnostics;

namespace Omada.Utils
{
    public class Helper
    {
        private IWebDriver _driver;

        private Actions actions;

        public Helper(IWebDriver driver)
        {
            _driver = driver;
            actions = new Actions(driver);
        }

        public string GetCurentTimespanAsString()
        {
            return DateTime.Now.ToString("MMddHHmmssff");
        }

        public string GetCurrentWindowHandle()
        {
            return _driver.CurrentWindowHandle;
        }

        private T WaitFor<T>(Func<IWebDriver, T> action, string description, int? timeout = null, bool logIt = true)
        {
            var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(int.Parse(ConfigurationManager.AppSettings["Timeout"])));
            var stopwatch = Stopwatch.StartNew();
            try
            {
                var result = wait.Until(d =>
                {
                    try
                    {
                        return action(d);
                    }
                    catch (Exception e)
                    {
                        Log.Logger.Information(e.Message.Split('\n').First());
                        return default(T);
                    }
                });
                stopwatch.Stop();
                if (logIt)
                    Log.Logger.Information($"Wait for {description} SUCCESS {(float)stopwatch.ElapsedMilliseconds / 1000:0.00}s");
                return result;
            }
            catch (Exception e)
            {
                stopwatch.Stop();
                throw new Exception($"Wait for {description} FAILED! {(float)stopwatch.ElapsedMilliseconds / 1000:0.00}s", e);
            }
        }

        public IWebElement WaitForElementWithText(By locator, string text)
        {
            return WaitFor(d =>
            {
                var elements = _driver.FindElements(locator);
                foreach (var element in elements)
                {
                    if (element.Text.Contains(text))
                    {
                        return element;
                    }
                }
                return null;
            }, $"text {text} on elements located by {locator} to be present");
        }

        public void WaitForFileToDownload(string filePath, long fileSize)
        {
            WaitFor(d =>
            {
                if (File.Exists(filePath))
                {
                    return true;
                }

                return false;
            }, $"file {filePath} with size {fileSize} to be downloaded");

            var file = new FileInfo(filePath);
            file.Length.Should().Be(fileSize);
        }

        public void CloseTab()
        {
            Log.Information("Closing tab");
            _driver.Close();
        }

        public IWebElement WaitForText(By locator, string text)
        {
            return WaitFor(d =>
            {
                var element = WaitForVisible(locator);
                if (element.Text.Contains(text))
                    return element;
                return null;
            }, $"text {text} on element element located by {locator} to be present");
        }

        public void OpenPage(string url)
        {
            if (_driver.Url != url)
            {
                Log.Information($"Opening ulr {url}");
                _driver.Navigate().GoToUrl(url);
            }
        }

        public IWebElement WaitAndSelectByText(By locator, string textToSelect)
        {
            var element = WaitForVisible(locator);

            return WaitFor(d =>
            {
                new SelectElement(element).SelectByText(textToSelect);
                return element;
            }, $"text {textToSelect} to be selected on element located by {locator}");
        }

        public bool WaitForInvisible(By locator)
        {
            return WaitFor(d =>
            {
                var element = _driver.FindElement(locator);
                return !element.Displayed;
            }, $"element element located by {locator} to be invisible");
        }

        public IWebElement GhostClick(By locator)
        {
            return WaitFor(d =>
            {
                var element = _driver.FindElement(locator);
                ((IJavaScriptExecutor)_driver).ExecuteScript("arguments[0].click()", element);
                return element;
            }, $"ghost clicking on element located by {locator}");
        }

        public string OpenNewTab()
        {
            Log.Information($"Openining new tab");
            ((IJavaScriptExecutor)_driver).ExecuteScript("window.open();");
            _driver.SwitchTo().Window(_driver.WindowHandles.Last());
            return _driver.CurrentWindowHandle;
        }

        public IWebElement WaitAndSendKeys(By locator, string text)
        {
            return WaitFor(d =>
            {
                var element = WaitForVisible(locator);
                element.SendKeys(text);
                return element;
            }, $"sending text {text} to element located by {locator}");
        }

        public IWebElement WaitForVisible(By locator)
        {
            return WaitFor(d =>
            {
                var element = _driver.FindElement(locator);
                if (element != null && element.Displayed)
                {
                    return element;
                }
                return null;
            }, $"an element located by {locator} to be visible");
        }

        public IWebElement WaitForElement(By locator)
        {
            return WaitFor(d =>
            {
                return _driver.FindElement(locator);
            }, $"element located by {locator}");
        }

        public IList<IWebElement> WaitForElements(By locator)
        {
            return WaitFor(d =>
            {
                var elements = _driver.FindElements(locator);
                if (elements.Any(element => !element.Displayed))
                {
                    return null;
                }

                return elements.Any() ? elements : null;
            }, $"elements located by {locator} to be visible");
        }

        public IWebElement ClickElementWithText(By locator, string text)
        {
            return WaitFor(d =>
            {
                var elements = _driver.FindElements(locator);
                foreach (var element in elements)
                {
                    if (element.Text.Contains(text))
                    {
                        element.Click();
                        return element;
                    }
                }
                return null;
            }, $"click on element with text {text} located by {locator}");
        }

        public IWebElement MoveToElementWithText(By locator, string text)
        {
            return WaitFor(d =>
            {
                var elements = _driver.FindElements(locator);

                foreach (var element in elements)
                {
                    if (element.Text.Contains(text))
                    {
                        actions.MoveToElement(element).Perform();
                        return element;
                    }
                }
                return null;
            }, $"move to element located by {locator}");
        }

        public IWebElement WaitAndClick(By locator)
        {
            var element = WaitForClickable(locator);
            return WaitFor(d =>
            {
                element.Click();
                return element;
            }, $"click to element located by {locator}");
        }

        public IWebElement WaitForClickable(By locator)
        {
            return WaitFor(d =>
            {
                var element = _driver.FindElement(locator);
                if (element != null && element.Enabled)
                {
                    return element;
                }
                else
                {
                    return null;
                }
            }, $"element located by {locator} to be clickable");
        }

        public bool AreAnyElements(By locator)
        {
            return _driver.FindElements(locator).Count > 0;
        }
    }
}
