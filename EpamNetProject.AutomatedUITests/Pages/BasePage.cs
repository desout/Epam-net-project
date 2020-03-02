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

        protected static IWebElement findElementBy(string selector, SelectorType type)
        {
            switch (type)
            {
                case SelectorType.Css:
                    return Driver.FindElement(By.CssSelector(selector));
                    break;
                case SelectorType.Id:
                    return Driver.FindElement(By.Id(selector));
                    break;
                case SelectorType.Tag:
                    return Driver.FindElement(By.TagName(selector));
                    break;
                case SelectorType.Xpath:
                    return Driver.FindElement(By.XPath(selector));
                    break;
                case SelectorType.Name:
                    return Driver.FindElement(By.Name(selector));
                    break;
                case SelectorType.ClassName:
                    return Driver.FindElement(By.ClassName(selector));
                    break;
                default:
                    return null;
            }
        }

        protected static IReadOnlyCollection<IWebElement> findElementsBy(string selector, SelectorType type)
        {
            switch (type)
            {
                case SelectorType.Css:
                    return Driver.FindElements(By.CssSelector(selector));
                    break;
                case SelectorType.Id:
                    return Driver.FindElements(By.Id(selector));
                    break;
                case SelectorType.Tag:
                    return Driver.FindElements(By.TagName(selector));
                    break;
                case SelectorType.Xpath:
                    return Driver.FindElements(By.XPath(selector));
                    break;
                case SelectorType.Name:
                    return Driver.FindElements(By.Name(selector));
                    break;
                case SelectorType.ClassName:
                    return Driver.FindElements(By.ClassName(selector));
                    break;
                default:
                    return null;
            }
        }
    }
}
