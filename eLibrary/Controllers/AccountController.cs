using AutoMapper;
using eLibrary.CommonService.Interface;
using eLibrary.Data;
using eLibrary.Modal;
using eLibrary.Modal.ViewModels;
using eLibrary.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

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
        private readonly IMapper mapper;
        private ICommonService _commonService;

        public AccountController(UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager, RoleManager<IdentityRole> roleManager, DataContext db, IMapper map,
            ICommonService commonService)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.roleManager = roleManager;
            this.dbInstance = db;
            this.mapper = map;
            this._commonService = commonService;
        }

        #region register

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            try
            {
                await _commonService.UserRegister(model);
                return StatusCode(201);
            }
            catch (Exception e)
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
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            try
            {
                var userDetails = await _commonService.UserLogin(model);
                return Ok(await Task.Run(() => userDetails));
                    
            }
            catch (Exception e)
            {
                if (e.Message.Contains(Constant.BlockedUser))
                {
                    ErrorModal error = new ErrorModal();
                    error.StatusCode = 400;
                    error.Message = Constant.BlockedUser;
                    return BadRequest(error);
                }
                else{
                    ErrorModal error = new ErrorModal();
                    error.StatusCode = 400;
                    error.Message = Constant.InvalidCredential;
                    return BadRequest(error);

                }
            }
        }
       
       
       
        #endregion

    }
}

