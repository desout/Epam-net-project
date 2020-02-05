using OpenQA.Selenium;

namespace EpamNetProject.AutomatedUITests.Pages
{
    public class EditEventPage
    {
        private const string NameTextFieldId = "Name";

        private const string DescriptionTextFieldId = "Description";

        private const string TimeTextFieldId = "Time";

        private const string TitleTextFieldId = "Title";

        private const string ImgUrlTextFieldId = "ImgUrl";

        private const string DatepickerCloseButtonClassname = "ui-datepicker-close";

        private const string SubmitButtonClassname = "button__submit";

        private readonly IWebElement _descriptionTextField;

        private readonly IWebDriver _driver;

        private readonly IWebElement _imgUrlField;

        private readonly IWebElement _nameTextField;

        private readonly IWebElement _submitButton;

        private readonly IWebElement _timeTextField;

        private readonly IWebElement _titleTextField;

        private EditEventPage(IWebDriver driver)
        {
            _driver = driver;
            _submitButton = _driver.FindElement(By.ClassName(SubmitButtonClassname));
            _imgUrlField = _driver.FindElement(By.Id(ImgUrlTextFieldId));
            _descriptionTextField = _driver.FindElement(By.Id(DescriptionTextFieldId));
            _nameTextField = _driver.FindElement(By.Id(NameTextFieldId));
            _timeTextField = _driver.FindElement(By.Id(TimeTextFieldId));
            _titleTextField = _driver.FindElement(By.Id(TitleTextFieldId));
        }

        public static EditEventPage GetPage(IWebDriver webDriver)
        {
            return new EditEventPage(webDriver);
        }

        public EditEventPage GoToPage()
        {
            return this;
        }

        public EditEventPage TypeNameField(string name)
        {
            FillField(_nameTextField, name);
            return this;
        }

        public EditEventPage TypeTitleField(string name)
        {
            FillField(_titleTextField, name);
            return this;
        }

        public EditEventPage TypeDescriptionField(string description)
        {
            FillField(_descriptionTextField, description);
            return this;
        }

        public EditEventPage TypeTimeField(string time)
        {
            FillField(_timeTextField, time);
            return this;
        }

        public EditEventPage TypeImgField(string img)
        {
            FillField(_imgUrlField, img);
            return this;
        }

        public EditEventPage CloseDatepicker()
        {
            _driver.FindElement(By.ClassName(DatepickerCloseButtonClassname)).Click();
            return this;
        }

        public EditEventPage ClickSubmitButton()
        {
            _submitButton.Click();
            return this;
        }

        private void FillField(IWebElement el, string field)
        {
            el.Clear();
            el.SendKeys(field);
        }
    }
}
