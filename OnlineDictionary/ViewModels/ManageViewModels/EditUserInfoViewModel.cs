using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNet.Identity;
using OnlineDictionary.Common;
using OnlineDictionaryResources;

namespace OnlineDictionary.ViewModels
{
    public class EditUserInfoViewModel
    {
        public bool HasPassword { get; set; }

        public IList<UserLoginInfo> Logins { get; set; }

        public string PhoneNumber { get; set; }

        public bool TwoFactor { get; set; }

        public bool BrowserRemembered { get; set; }

        [EmailAddress]
        [Required(ErrorMessageResourceName = "RequiredErrorMessage", ErrorMessageResourceType = typeof(Lexicon))]
        [Display(ResourceType = typeof(Lexicon), Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessageResourceName = "RequiredErrorMessage", ErrorMessageResourceType = typeof(Lexicon))]
        [Display(ResourceType = typeof(Lexicon), Name = "FirstName")]
        public string FirstName { get; set; }

        [Required(ErrorMessageResourceName = "RequiredErrorMessage", ErrorMessageResourceType = typeof(Lexicon))]
        [Display(ResourceType = typeof(Lexicon), Name = "LastName")]
        public string LastName { get; set; }

        [Required(ErrorMessageResourceName = "RequiredErrorMessage", ErrorMessageResourceType = typeof(Lexicon))]
        [Display(ResourceType = typeof(Lexicon), Name = "Day")]
        public int BirthDay { get; set; }

        [Required(ErrorMessageResourceName = "RequiredErrorMessage", ErrorMessageResourceType = typeof(Lexicon))]
        [Display(ResourceType = typeof(Lexicon), Name = "Month")]
        public int BirthMonth { get; set; }

        [Required(ErrorMessageResourceName = "RequiredErrorMessage", ErrorMessageResourceType = typeof(Lexicon))]
        [Display(ResourceType = typeof(Lexicon), Name = "Year")]
        public int BirthYear { get; set; }

        [Required(ErrorMessageResourceName = "RequiredErrorMessage", ErrorMessageResourceType = typeof(Lexicon))]
        [Display(ResourceType = typeof(Lexicon), Name = "Country")]
        public string Country { get; set; }

        [Display(ResourceType = typeof(Lexicon), Name = "Language")]
        public string Language { get; set; }
    }
}