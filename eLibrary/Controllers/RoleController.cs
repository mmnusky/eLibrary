using eLibrary.Data;
using eLibrary.Modal;
using eLibrary.Modal.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace eLibrary.Controllers
{
    [Route("api/v1/[controller]/[action]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly DataContext dbInstance;

        public RoleController(UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager, RoleManager<IdentityRole> roleManager, DataContext db)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.roleManager = roleManager;
            this.dbInstance = db;
        }

        [HttpPost]
        [Authorize("Admin")]
        public async Task<string> InsertRoles([FromBody] RoleViewModel roleViewModel)
        {
            try
            {
                //bool cc = User.Identity.IsAuthenticated;

                var user = dbInstance.ApplicationUser.Where(s => s.Email == roleViewModel.UserEmail).Single();
                var role = new IdentityRole
                {
                    Id = user.Id,
                    Name = roleViewModel.RoleName
                };
                var isRoleExist = await roleManager.RoleExistsAsync(roleViewModel.RoleName);
                if (!isRoleExist)
                {
                    await roleManager.CreateAsync(role);
                }

                await userManager.AddToRoleAsync(user, roleViewModel.RoleName);
                return $"{roleViewModel.RoleName} role has been added to {user.Email} sucessfully";
            }
            catch (Exception ex)
            {
                //log exception TBD
                throw ex;
            }
        }
        [HttpPost]
        [Authorize("Admin")]
        public async Task<string> DeactivateAuthor(string userId)
        {
            try
            {
                if (!string.IsNullOrEmpty(userId))
                {
                    //var user = userManager.FindByIdAsync(userId).sing;
                    var user = dbInstance.ApplicationUser.Where(s => s.Id == userId).Single();
                    user.IsActive = true;
                    await userManager.UpdateAsync(user);
                    return $"User deactivated successfully";
                }
                else
                {
                    throw new Exception("User id is null or empty");
                }
            }
            catch (Exception ex) 
            {
                //log exception TBD
                throw ex;
            }

        }
    }
}
