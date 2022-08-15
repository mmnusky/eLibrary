using eLibrary.CommonService.Interface;
using eLibrary.Data;
using eLibrary.Modal;
using eLibrary.Modal.ViewModels;
using eLibrary.Repository.Interface;
using eLibrary.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;

namespace eLibrary.Controllers
{
    [Route("api/v1/[controller]/[action]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly DataContext dbInstance;
        private IRepositoryWrapper _repository;
        private ICommonService _commonService;

        public RoleController(UserManager<ApplicationUser> userManager, IRepositoryWrapper repository,
            RoleManager<IdentityRole> roleManager, DataContext db,
            ICommonService commonService)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.dbInstance = db;
            this._repository = repository;
            this._commonService = commonService;
        }
     
        [HttpPost]
        [Authorize(AuthenticationSchemes = Constant.Bearer, Roles = Constant.Admin)]
        public async Task<IActionResult> DeactivateAuthor([FromBody] AuthorDeactivateRequest deactivateRequest)
        {
            try
            {
                if (!string.IsNullOrEmpty(deactivateRequest.Email))
                {
                    await _commonService.Deactivate(deactivateRequest.Email);
                    return Ok();
                }
                else
                {
                    throw new Exception(Constant.EmptyOrNullUser);
                }
            }
            catch (Exception ex) 
            {
                if (ex.Message.Contains(Constant.EmptyOrNullUser))
                {
                    ErrorModal error = new ErrorModal();
                    error.StatusCode = 404;
                    error.Message = Constant.EmptyOrNullUser;
                    return NotFound(error);
                }
                else
                {
                    ErrorModal error = new ErrorModal();
                    error.StatusCode = 400;
                    error.Message = Constant.StatusChangeError;
                    return BadRequest(error);
                }
                
            }

        }
        [HttpGet]
        [Authorize(AuthenticationSchemes = Constant.Bearer, Roles = Constant.Admin)]
        public async Task<IActionResult> GetAllAuthor()
        {
            try
            {
                var allAuthor = _commonService.GetAllAuthors();
                return Ok(await Task.Run(() => allAuthor));
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains(Constant.NoActiveAuthorFound))
                {
                    ErrorModal error = new ErrorModal();
                    error.StatusCode = 404;
                    error.Message = Constant.NoAuthorFound;
                    return NotFound(error);
                }
                else
                {
                    ErrorModal error = new ErrorModal();
                    error.StatusCode = 400;
                    error.Message = Constant.GetAllAuthorError;
                    return BadRequest(error);
                }
            }

        }
        [HttpGet]
        [Authorize(AuthenticationSchemes = Constant.Bearer, Roles = Constant.AdminAuthor)]
        public async Task<IActionResult> GetAllActiveAuthor()
        {
            try
            {
                var allAuthor = await userManager.GetUsersInRoleAsync(Constant.Author);
                var activeAuthor = allAuthor.Where(act => act.IsDeactivated == false);
                if (activeAuthor.Count() == 0)
                {
                    throw new Exception(Constant.NoActiveAuthorFound);
                }

                return Ok(await Task.Run(() => activeAuthor));
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains(Constant.NoActiveAuthorFound))
                {
                    ErrorModal error = new ErrorModal();
                    error.StatusCode = 404;
                    error.Message = Constant.NoActiveAuthorFound;
                    return BadRequest(error);
                }
                else
                {
                    ErrorModal error = new ErrorModal();
                    error.StatusCode = 400;
                    error.Message = Constant.GetActiveAuthorError;
                    return BadRequest(error);
                }
            }

        }

    }
}
