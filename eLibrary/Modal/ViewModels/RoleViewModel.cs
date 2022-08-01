using System.ComponentModel.DataAnnotations;

namespace eLibrary.Modal.ViewModels
{
    public class RoleViewModel
    {

        [Required]
        [Display(Name = "Role")]
        public string RoleName { get; set; }
        [Required]
        [Display(Name = "User Name")]
        public string UserEmail { get; set; }


    }
}
