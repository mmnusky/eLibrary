using eLibrary.Data;
using eLibrary.Modal;
using eLibrary.Repository.Interface;
using Microsoft.AspNetCore.Identity;

namespace eLibrary.Repository
{
    public class RoleRepository : RepositoryBase<ApplicationUser>, IRoleRepository
    {
        public RoleRepository(DataContext repositoryContext)
            : base(repositoryContext)
        {
        }
    }
}
