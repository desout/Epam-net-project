using System;
using System.Collections.Generic;
using System.Configuration;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace EpamNetProject.AutomatedUITests.Pages
{
    public class BasePage : IDisposable
    {
        protected static IWebDriver Driver;

        public BasePage()
        {
            if (Driver != null)
            {
                return;
            }

            Driver = new ChromeDriver();
            Driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            Driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(10);
            Driver.Manage().Window.Maximize();
            Driver.Navigate().GoToUrl(ConfigurationManager.AppSettings["rootUrl"]);
        }

        public void Dispose()
        {
        }

        public static void RemoveDriver()
        {
            if (Driver != null)
            {
                Driver.Quit();
                Driver.Dispose();
                Driver = null;
            }
        }

        protected static IWebElement FindElementById(string selector)
        {
            return Driver.FindElement(By.Id(selector));
        }

        protected static IWebElement FindElementByCss(string selector)
        {
            return Driver.FindElement(By.CssSelector(selector));
        }

        protected static IWebElement FindElementByClassName(string selector)
        {
            return Driver.FindElement(By.ClassName(selector));
        }

        protected static IWebElement FindElementByXPath(string selector)
        {
            return Driver.FindElement(By.XPath(selector));
        }

        protected static IReadOnlyCollection<IWebElement> FindElementsByXPath(string selector)
        {
            return Driver.FindElements(By.XPath(selector));
        }

        protected static IReadOnlyCollection<IWebElement> FindElementsByCss(string selector)
        {
            return Driver.FindElements(By.CssSelector(selector));
        }

        protected static IReadOnlyCollection<IWebElement> FindElementsByClassName(string selector)
        {
            return Driver.FindElements(By.ClassName(selector));
        }
    }
}
