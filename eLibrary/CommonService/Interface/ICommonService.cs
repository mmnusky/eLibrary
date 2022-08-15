using eLibrary.Modal;
using eLibrary.Modal.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace eLibrary.CommonService.Interface
{
    public interface ICommonService
    {
        Task<string> Deactivate(string email);
        Task<IList<ApplicationUser>> GetAllAuthors();
        Task<IList<ApplicationUser>> GetActiveAuthors();
        Task<string> UserRegister(RegisterViewModel model);
        Task<LoginResponseDto> UserLogin(LoginViewModel model);
    }
}
