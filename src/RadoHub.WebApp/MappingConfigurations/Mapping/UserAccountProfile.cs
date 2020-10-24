using AutoMapper;
using RadoHub.Data.Models;
using RadoHub.ViewModels.UserAccount;

namespace RadoHub.Services.MappingConfigurations.Mapping
{
    public class UserAccountProfile : Profile
    {
        public UserAccountProfile()
        {
            this.CreateMap<RadoHubUser, UserAccountAdminViewModel>();
        }
    }
}
