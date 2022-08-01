using System.ComponentModel.DataAnnotations;

namespace eLibrary.Modal.ViewModels
{
    public class LoginViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Is Active")]
        public bool IsActive { get; set; }
    }
}
