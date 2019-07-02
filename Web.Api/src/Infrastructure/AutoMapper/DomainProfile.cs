using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Web.Api.Modules.Auth;
using Web.Api.Modules.Auth.Domain;
using Web.Api.Modules.Auth.Dtos;

namespace Web.Api.Infrastructure.AutoMapper
{
    public class DomainProfile: Profile
    {
        public DomainProfile()
        {
            CreateMap<User, UserDto>();
            CreateMap<UserDto, User>();
            CreateMap<SignupDto, User>();
            CreateMap<User, SignupDto>();
            CreateMap<Phone, PhoneDto>();
            CreateMap<PhoneDto, Phone>();
        }
    }
}
