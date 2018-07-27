using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;
using System.Web;

namespace OnlineDictionary.Common
{
    public class Language
    {
        public string LangFullName { get; set; }

        public string LangCultureName { get; set; }
    }

    public class LanguagesManager
    {
        public static List<Language> AvailableLanguages
        {
            get
            {
                return new List<Language>
                {
                    new Language { LangFullName = "English", LangCultureName = "en-US" },
                    new Language { LangFullName = "Українська", LangCultureName = "uk-UA" },
                    new Language { LangFullName = "Русский", LangCultureName = "ru-RU" }
                };
            }
        }

        public static bool IsLanguageAvailable(string Language)
        {
            return AvailableLanguages.Exists(l => l.LangCultureName.Equals(Language));
        }

        public static string GetDefaultLanguage()
        {
            return AvailableLanguages[0].LangCultureName;
        }

        public static void SetLanguage(string lang)
        {
            try
            {
                if (!IsLanguageAvailable(lang)) lang = GetDefaultLanguage();
                CultureInfo cultureInfo = new CultureInfo(lang);
                Thread.CurrentThread.CurrentUICulture = cultureInfo;
                Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(cultureInfo.Name);
                OnlineDictionaryResources.Lexicon.Culture = cultureInfo;

                HttpCookie langCookie = new HttpCookie("culture", lang)
                {
                    Expires = DateTime.Now.AddYears(1)
                };
                HttpContext.Current.Response.Cookies.Remove("culture");
                HttpContext.Current.Response.Cookies.Add(langCookie);
            }
            catch (Exception)
            {
                //add log
                throw;
            }
        }
    }
}