using eLibrary.Data;
using eLibrary.Modal;
using eLibrary.Modal.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace eLibrary.Controllers
{

    [Route("api/v1/[controller]/[action]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly DataContext dbInstance;

        public AccountController(UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager, RoleManager<IdentityRole> roleManager, DataContext db)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.roleManager = roleManager;
            this.dbInstance = db;
        }

        #region register

        [HttpPost]
        public async Task<string> Register(RegisterViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    // Copy data from RegisterViewModel to IdentityUser
                    var user = new ApplicationUser
                    {
                        UserName = model.Email,
                        Email = model.Email
                    };
                    var result = await userManager.CreateAsync(user, model.Password);
                    if (result.Succeeded)
                    {
                        return $"{model.FirstName} has been successfully registered to eLibrary";
                    }
                    else
                    {
                        //Exception handling TBD
                        return "Failed to register user";
                    }
                }

                return "Invalid data has been provided to register";
            }
            catch(Exception e)
            {
                throw e;
            }
        }
        #endregion

        #region logout
        [HttpPost]
        public async Task<string> Logout()
        {
            await signInManager.SignOutAsync();
            return "Successfully logged out";
        }
        #endregion

        #region login
        [HttpPost]
        public async Task<string> Login(LoginViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var userList = dbInstance.ApplicationUser.ToList();
                    var user = userList.FirstOrDefault(s => s.Email == model.Email);
                    if (user == null || user.IsActive == false)
                    {
                        var result = await signInManager.PasswordSignInAsync(
                            model.Email, model.Password, true, false);

                        if (result.Succeeded)
                        {
                            return "Successfully logged in";
                        }
                    }
                    else
                    {
                        return "In valid user";
                    }

                    ModelState.AddModelError(string.Empty, "Invalid Login Attempt");
                }

                return "In valid credential has been provided";
            }
            catch(Exception e) {
                throw e;
            }
        }
        #endregion

    }
}

