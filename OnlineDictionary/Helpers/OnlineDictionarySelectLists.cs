using OnlineDictionary.Common;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web.Mvc;

namespace OnlineDictionary.Helpers
{
    public static class OnlineDictionarySelectLists
    {
        public static IEnumerable<SelectListItem> Countries
        {
            get
            {
                List<string> countries = new List<string>();
                foreach (CultureInfo ci in CultureInfo.GetCultures(CultureTypes.SpecificCultures))
                {
                    var ri = new RegionInfo(ci.Name);
                    countries.Add(ri.DisplayName);
                }
                countries = countries.Distinct().ToList();
                countries.Sort();

                string currentCountry = RegionInfo.CurrentRegion.DisplayName;
                return new SelectList(countries, currentCountry);
            }
        }

        public static IEnumerable<SelectListItem> Languages
        {
            get
            {
                List<string> languages = new List<string>();
                foreach (CultureInfo ci in CultureInfo.GetCultures(CultureTypes.NeutralCultures))
                {
                    languages.Add(ci.DisplayName);
                }
                languages = languages.Distinct().ToList();
                languages.Sort();

                //or get NativeName
                string currentLanguage = CultureInfo.CurrentCulture.DisplayName;
                return new SelectList(languages, currentLanguage);
            }
        }

        public static IEnumerable<SelectListItem> AvailableLanguages
        {
            get
            {
                string selectedLanguage = LanguagesManager.GetDefaultLanguage();
                string threadLanguage = Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;
                if (LanguagesManager.AvailableLanguages.Exists(l => l.LangCultureName == threadLanguage))
                {
                    selectedLanguage = threadLanguage;
                }

                return new SelectList(LanguagesManager.AvailableLanguages, "LangCultureName", "LangFullName", selectedLanguage);
            }
        }
    }
}