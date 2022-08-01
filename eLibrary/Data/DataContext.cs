using eLibrary.Modal;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace eLibrary.Data
{
    public class DataContext : IdentityDbContext<ApplicationUser>
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<BookDetailsModal> BookDetails { get; set; }
        public DbSet<ApplicationUser> ApplicationUser { get; set; }

    }
}
