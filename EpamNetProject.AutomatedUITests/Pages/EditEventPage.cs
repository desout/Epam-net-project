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

        private static IWebElement DescriptionTextField => FindElementById(DescriptionTextFieldId);

        private static IWebElement ImgUrlField => FindElementById(ImgUrlTextFieldId);

        private static IWebElement NameTextField => FindElementById(NameTextFieldId);

        private static IWebElement SubmitButton => FindElementByClassName(SubmitButtonClassname);

        private static IWebElement TimeTextField => FindElementById(TimeTextFieldId);

        private static IWebElement TitleTextField => FindElementById(TitleTextFieldId);

        private static IWebElement DatePickerCloseButton => FindElementByClassName(DatepickerCloseButtonClassname);
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
            DatePickerCloseButton.Click();
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
