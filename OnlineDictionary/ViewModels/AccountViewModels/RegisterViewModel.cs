using OnlineDictionaryResources;
using System;
using System.ComponentModel.DataAnnotations;

namespace OnlineDictionary.ViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessageResourceName = "RequiredErrorMessage", ErrorMessageResourceType = typeof(Lexicon))]
        [EmailAddress]
        [Display(ResourceType = typeof(Lexicon), Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessageResourceName = "RequiredErrorMessage", ErrorMessageResourceType = typeof(Lexicon))]
        [StringLength(100, ErrorMessageResourceName = "PasswordLengthErrorMessage", ErrorMessageResourceType = typeof(Lexicon), MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(ResourceType = typeof(Lexicon), Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessageResourceName = "PasswordCompareErrorMessage", ErrorMessageResourceType = typeof(Lexicon))]
        [Display(ResourceType = typeof(Lexicon), Name = "ConfirmPassword")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessageResourceName = "RequiredErrorMessage", ErrorMessageResourceType = typeof(Lexicon))]
        [Display(ResourceType = typeof(Lexicon), Name = "UserName")]
        public string UserName { get; set; }

        [Required(ErrorMessageResourceName = "RequiredErrorMessage", ErrorMessageResourceType = typeof(Lexicon))]
        [Display(ResourceType = typeof(Lexicon), Name = "FirstName")]
        public string FirstName { get; set; }

        [Required(ErrorMessageResourceName = "RequiredErrorMessage", ErrorMessageResourceType = typeof(Lexicon))]
        [Display(ResourceType = typeof(Lexicon), Name = "LastName")]
        public string LastName { get; set; }

        [Required(ErrorMessageResourceName = "RequiredErrorMessage", ErrorMessageResourceType = typeof(Lexicon))]
        [Display(ResourceType = typeof(Lexicon), Name = "BirthDate")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }

        [Display(ResourceType = typeof(Lexicon), Name = "Country")]
        public string Country { get; set; }

        [Display(ResourceType = typeof(Lexicon), Name = "Language")]
        public string Language { get; set; }
    }
}
