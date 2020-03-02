using OpenQA.Selenium;

namespace EpamNetProject.AutomatedUITests.Pages
{
    public class EditEventPage : BasePage
    {
        private const string NameTextFieldId = "Name";

        private const string DescriptionTextFieldId = "Description";

        private const string TimeTextFieldId = "Time";

        private const string TitleTextFieldId = "Title";

        private const string ImgUrlTextFieldId = "ImgUrl";

        private const string DatepickerCloseButtonClassname = "ui-datepicker-close";

        private const string SubmitButtonClassname = "button__submit";

        private static IWebElement DescriptionTextField => findElementBy(DescriptionTextFieldId, SelectorType.Id);

        private static IWebElement ImgUrlField => findElementBy(ImgUrlTextFieldId, SelectorType.Id);

        private static IWebElement NameTextField => findElementBy(NameTextFieldId, SelectorType.Id);

        private static IWebElement SubmitButton => findElementBy(SubmitButtonClassname, SelectorType.ClassName);

        private static IWebElement TimeTextField => findElementBy(TimeTextFieldId, SelectorType.Id);

        private static IWebElement TitleTextField => findElementBy(TitleTextFieldId, SelectorType.Id);

        public EditEventPage()
        {
        }

        public EditEventPage TypeNameField(string name)
        {
            FillField(NameTextField, name);
            return this;
        }

        public EditEventPage TypeTitleField(string name)
        {
            FillField(TitleTextField, name);
            return this;
        }

        public EditEventPage TypeDescriptionField(string description)
        {
            FillField(DescriptionTextField, description);
            return this;
        }

        public EditEventPage TypeTimeField(string time)
        {
            FillField(TimeTextField, time);
            return this;
        }

        public EditEventPage TypeImgField(string img)
        {
            FillField(ImgUrlField, img);
            return this;
        }

        public EditEventPage CloseDatepicker()
        {
            findElementBy(DatepickerCloseButtonClassname, SelectorType.ClassName).Click();
            return this;
        }

        public EditEventPage ClickSubmitButton()
        {
            SubmitButton.Click();
            return this;
        }

        private void FillField(IWebElement el, string field)
        {
            el.Clear();
            el.SendKeys(field);
        }
    }
}
