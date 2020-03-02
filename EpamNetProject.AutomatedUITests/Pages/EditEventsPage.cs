using System.Threading;
using OpenQA.Selenium;

namespace EpamNetProject.AutomatedUITests.Pages
{
    public class EditEventsPage : BasePage
    {
        private const string ParagraphClassname = "event--item__name";

        private const string AddNewClassname = "editEvent__block__AddNew";
        
        private const string BlockClassname = "event--item__actions";

        private static IWebElement AddNewButton => findElementBy(AddNewClassname, SelectorType.ClassName);


        public EditEventsPage()
        {
        }

        public EditEventsPage ClickDeleteButtonOnEvent(string text)
        {
            var element = findElementBy(GetEventXPathByText(text), SelectorType.Xpath)
                .FindElement(By.TagName("button"));
            element.Click();
            Thread.Sleep(1000);
            return this;
        }

        public EditEventPage ClickEditLinkOnEvent(string text)
        {
            var element = findElementBy(
                    GetEventXPathByText(text), SelectorType.Xpath)
                .FindElement(By.TagName("a"));
            element.Click();
            return new EditEventPage();
        }

        public EditEventPage ClickAddNewButton()
        {
            AddNewButton.Click();
            return new EditEventPage();
        }

        private static string GetEventXPathByText(string text)
        {
            return
                $"//*[@class='{ParagraphClassname}' and text()='{text}']/following-sibling::div[@class='{BlockClassname}']";
        }
    }
}
