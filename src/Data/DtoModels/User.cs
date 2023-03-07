using AutoMapper;

namespace SimpleRedirects.Data.DtoModels;

public class User : Core.Entities.User
{
    
}

public class AppUserMapperProfile : Profile
{
    public AppUserMapperProfile()
    {
        CreateMap<Core.Entities.User, User>().ReverseMap();
    }
}