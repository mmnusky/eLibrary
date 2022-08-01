using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace eLibrary.Modal.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        [EmailAddress]
        [JsonPropertyName("email")]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [JsonPropertyName("password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password",
            ErrorMessage = "Password and confirmation password do not match.")]
        [JsonPropertyName("confirmpassword")]
        public string ConfirmPassword { get; set; }
        [Required]
        [JsonPropertyName("firstname")]
        public string FirstName { get; set; }
        [Required]
        [JsonPropertyName("lastname")]
        public string LastName { get; set; }

    }
}
