﻿using OnlineDictionary.Common;
using OnlineDictionaryResources;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Threading;

namespace OnlineDictionary.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        [EmailAddress]
        [Display(ResourceType = typeof(Lexicon), Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(ResourceType = typeof(Lexicon), Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        [Display(ResourceType = typeof(Lexicon), Name = "ConfirmPassword")]
        public string ConfirmPassword { get; set; }

        [Display(ResourceType = typeof(Lexicon), Name = "UserName")]
        public string UserName { get; set; }

        [Display(ResourceType = typeof(Lexicon), Name = "FirstName")]
        public string FirstName { get; set; }

        [Display(ResourceType = typeof(Lexicon), Name = "LastName")]
        public string LastName { get; set; }

        [Display(ResourceType = typeof(Lexicon), Name = "Day")]
        public int BirthDay { get; set; }

        [Display(ResourceType = typeof(Lexicon), Name = "Month")]
        public int BirthMonth { get; set; }

        [Display(ResourceType = typeof(Lexicon), Name = "Year")]
        public int BirthYear { get; set; }

        [Display(ResourceType = typeof(Lexicon), Name = "Country")]
        public string Country { get; set; }

        [Display(ResourceType = typeof(Lexicon), Name = "Language")]
        public string Language { get; set; }

        public static IEnumerable<System.Web.Mvc.SelectListItem> Countries
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

                return new System.Web.Mvc.SelectList(countries, currentCountry);
            }
        }

        public static IEnumerable<System.Web.Mvc.SelectListItem> LanguageItems
        {
            get
            {
                string selectedLanguage = Languages.GetDefaultLanguage();
                string threadLanguage = Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;
                if (Languages.AvailableLanguages.Exists(l => l.LangCultureName == threadLanguage))
                {
                    selectedLanguage = threadLanguage;
                }

                return new System.Web.Mvc.SelectList(Languages.AvailableLanguages, "LangCultureName", "LangFullName", selectedLanguage); 
            }
        }
    }
}
