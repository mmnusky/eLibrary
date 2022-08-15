using AutoMapper;
using eLibrary.CommonService.Interface;
using eLibrary.Data;
using eLibrary.Modal;
using eLibrary.Modal.ViewModels;
using eLibrary.Repository.Interface;
using eLibrary.Shared;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace eLibrary.CommonService
{
    public class CommonService : ICommonService
    {
        private readonly DataContext dbInstance;
        private IRepositoryWrapper _repository;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IMapper _mapper;
        private readonly SignInManager<ApplicationUser> _signInManager;
        public CommonService(DataContext db, IRepositoryWrapper repository, UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager, IMapper mapper, SignInManager<ApplicationUser> signInManager)
        {
            this.dbInstance = db;
            this._repository = repository;
            this._userManager = userManager;
            this._roleManager = roleManager;
            this._mapper = mapper;
            this._signInManager = signInManager;
        }

        public async Task<string> Deactivate(string email)
        {
            // var user = dbInstance.ApplicationUser.Where(s => s.Email == email).Single();
            var user = await _repository.RoleRepository.GetUser(email);
            user.IsDeactivated = user.IsDeactivated ? false : true;
            await _repository.RoleRepository.Update(user);
            return string.Empty;
        }
        public async Task<IList<ApplicationUser>> GetAllAuthors()
        {
            try
            {
                var allAuthor = await _userManager.GetUsersInRoleAsync(Constant.Author);
                if (allAuthor.Count() == 0)
                {
                    throw new Exception(Constant.NoActiveAuthorFound);
                }
                return await Task.Run(() => allAuthor);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<IList<ApplicationUser>> GetActiveAuthors()
        {
            try
            {
                var allAuthor = await _userManager.GetUsersInRoleAsync(Constant.Author);
                var activeAuthor = allAuthor.Where(act => act.IsDeactivated == false);
                if (activeAuthor.Count() == 0)
                {
                    throw new Exception(Constant.NoActiveAuthorFound);
                }

                return (IList<ApplicationUser>)await Task.Run(() => activeAuthor);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<string> UserRegister(RegisterViewModel model)
        {
            try {
                var user = _mapper.Map<RegisterViewModel, ApplicationUser>(model);
                var result = await _userManager.CreateAsync(user, model.Password);
                if (!result.Succeeded)
                {
                    var errors = result.Errors.Select(e => e.Description);

                    throw new Exception("Error while registering the User");
                }
                var role = new IdentityRole
                {
                    Id = user.Id,
                    Name = model.Role
                };
                var isRoleExist = await _roleManager.RoleExistsAsync(model.Role);
                if (!isRoleExist)
                {
                    await _roleManager.CreateAsync(role);
                }
                await _userManager.AddToRoleAsync(user, model.Role);

                return "Succrssfully Registered";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<LoginResponseDto> UserLogin(LoginViewModel model)
        {
            try {
                var userList = dbInstance.ApplicationUser.ToList();
                var user = userList.FirstOrDefault(s => s.Email == model.Email);
                if (user == null || user.IsDeactivated == false)
                {
                    var result = await _signInManager.PasswordSignInAsync(
                        model.Email, model.Password, true, false);

                    if (result.Succeeded)
                    {
                        var myuser = dbInstance.ApplicationUser.Where(s => s.Email == model.Email).Single();
                        var userDetails = _mapper.Map<ApplicationUser, LoginResponseDto>(myuser);
                        // string rolename = await userManager.GetRolesAsync
                        var roles = await _userManager.GetRolesAsync(myuser);
                        var roleName = roles.ToList().FirstOrDefault();
                        userDetails.Role = roleName;
                        userDetails.Token = GetToken(userDetails);

                        return await Task.Run(() => userDetails);
                    }
                    else
                    {
                        throw new Exception(Constant.InvalidCredential);
                    }
                }
                else
                {
                    throw new Exception(Constant.BlockedUser);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            }

        private string GetToken(LoginResponseDto authUser)
        {
            string role = !string.IsNullOrEmpty(authUser.Role) ? authUser.Role : "NoRole";

            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, authUser.Email),
                new Claim(ClaimTypes.Role,role)
            };
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Constant.key));
            var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
            var tokeOptions = new JwtSecurityToken(
                issuer: Constant.ValidIssuer,
                audience: Constant.ValidAudience,
                claims: claims,
                expires: DateTime.Now.AddMinutes(50),
                signingCredentials: signinCredentials
            );
            var tokenString = new JwtSecurityTokenHandler().WriteToken(tokeOptions);
            return tokenString;

        }
    }  
    }