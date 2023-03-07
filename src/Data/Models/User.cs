using AutoMapper;

namespace SimpleRedirects.Data.Models;

public class User : Core.Entities.User
{
}

public class UserMapperProfile : Profile
{
    public UserMapperProfile()
    {
        CreateMap<Core.Entities.User, User>().ReverseMap();
    }
}