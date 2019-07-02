using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Api.Modules.Auth;
using Web.Api.Modules.Auth.Domain;
using Web.Api.Modules.Auth.Dtos;
using Web.Api.Modules.Auth.Validators;
using Web.Api.src.Modules.Auth.Validators;

namespace Web.Api.Infrastructure.Pipes
{
    public static class ValidatorPipe
    {
        public static IServiceCollection AddValidators(this IServiceCollection services)
        {
            services.AddTransient<IValidator<UserDto>, UserDtoValidator>();
            services.AddTransient<IValidator<SigninDto>, SigninDtoValidator>();
            services.AddTransient<IValidator<SignupDto>, SignupDtoValidator>();
            services.AddTransient<IValidator<PhoneDto>, PhoneDtoValidator>();

            return services;
        }
    }
}
