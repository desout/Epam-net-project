using System.Collections.Generic;

namespace EpamNetProject.PLL.Statics
{
    public static class StaticData
    {
        public static List<Language> Languages = new List<Language>
        {
            new Language {Code = "en", Description = "English"}, new Language {Code = "ru", Description = "Русский"},
            new Language {Code = "blr", Description = "Беларуски"}
        };
    }

    public class Language
    {
        public string Code { get; set; }
        public string Description { get; set; }
    }
}
