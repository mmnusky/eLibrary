using AutoMapper;
using eLibrary.Modal;
using eLibrary.Modal.ViewModels;

namespace eLibrary.AutoMapper
{
    public class AutoMapping : Profile

    {
        public AutoMapping()
        {
            CreateMap<ApplicationUser, LoginResponseDto>()
       .ForMember(u => u.FirstName, opt => opt.MapFrom(x => x.FirstName))
       .ForMember(u => u.Email, opt => opt.MapFrom(x => x.Email))
       .ForMember(u => u.userID, opt => opt.MapFrom(x => x.Id));

            CreateMap<RegisterViewModel, ApplicationUser>()
      .ForMember(u => u.FirstName, opt => opt.MapFrom(x => x.FirstName))
      .ForMember(u => u.UserName, opt => opt.MapFrom(x => x.Email))
      .ForMember(u => u.Email, opt => opt.MapFrom(x => x.Email))
      .ForMember(u => u.LastName, opt => opt.MapFrom(x => x.LastName));

        }
    }
}
