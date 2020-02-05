using System.Threading;
using OpenQA.Selenium;

namespace EpamNetProject.AutomatedUITests.Pages
{
    public class EditEventsPage
    {
        private const string PageLink = "http://localhost:5000/Manager/EditEvent/EditEvents";

        private const string ParagraphClassname = "event--item__name";

        private const string BlockClassname = "event--item__actions";

        private readonly IWebDriver _driver;

        private EditEventsPage(IWebDriver driver)
        {
            _driver = driver;
        }

        public static EditEventsPage GetPage(IWebDriver webDriver)
        {
            return new EditEventsPage(webDriver);
        }

        public EditEventsPage GoToPage()
        {
            _driver.Navigate().GoToUrl(PageLink);
            return this;
        }

        public EditEventsPage ClickDeleteButtonOnEvent(string text)
        {
            var element = _driver
                .FindElement(By.XPath(
                    $"//*[@class='{ParagraphClassname}' and text()='{text}']/following-sibling::div[@class='{BlockClassname}']"))
                .FindElement(By.TagName("button"));
            element.Click();
            Thread.Sleep(1000);
            return this;
        }

        public EditEventPage ClickEditLinkOnEvent(string text)
        {
            var element = _driver
                .FindElement(By.XPath(
                    $"//*[@class='{ParagraphClassname}' and text()='{text}']/following-sibling::div[@class='{BlockClassname}']"))
                .FindElement(By.TagName("a"));
            element.Click();
            return EditEventPage.GetPage(_driver);
        }

        public EditEventPage ClickAddNewButton()
        {
            _driver.FindElement(By.ClassName("editEvent__block__AddNew")).Click();
            return EditEventPage.GetPage(_driver);
        }
    }
}
