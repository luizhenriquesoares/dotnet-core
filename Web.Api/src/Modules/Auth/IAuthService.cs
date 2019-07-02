using FluentValidation;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Api.Modules.Auth.Domain;
using Web.Api.Modules.Auth.Dtos;

namespace Web.Api.Modules.Auth
{
    public interface IAuthService
    {
        Task<IdentityResult> CreateAsync(SignupDto dto);

        Task<SignInResult> AuthenticateAsync(SigninDto dto);

        Task<User> FindAsync(SigninDto dto);

        Task<UserDto> GetUserToken(string token);


    }
}
