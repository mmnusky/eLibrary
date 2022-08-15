using Microsoft.AspNetCore.Identity;

namespace eLibrary.Modal
{
    public class ApplicationUser : IdentityUser
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public bool IsDeactivated { get; set; }
       // public ICollection<BookDetailsModal> BookDetailsModal { get; set; }
    }
}
