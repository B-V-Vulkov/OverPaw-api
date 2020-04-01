namespace OverPaw.Services.Infrastructure
{
    using AutoMapper;

    using Models;
    using Data.Models;
    using System.Linq;

    public class ServiceMappingProfile : Profile
    {
        public ServiceMappingProfile()
        {
            this.CreateMap<User, TestUserServiceModel>();

            this.CreateMap<Post, PostServiseModel>();
        }
    }
}
