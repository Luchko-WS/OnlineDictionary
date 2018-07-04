using System.ComponentModel.DataAnnotations;

namespace OnlineDictionary.ViewModels
{
    public class ForgotViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}
