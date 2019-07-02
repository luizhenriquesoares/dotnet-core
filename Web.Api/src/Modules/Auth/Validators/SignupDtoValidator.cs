using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Api.Modules.Auth;
using Web.Api.Modules.Auth.Dtos;

namespace Web.Api.src.Modules.Auth.Validators
{
    public class SignupDtoValidator : AbstractValidator<SignupDto>
    {
        public SignupDtoValidator(IValidator<PhoneDto> validator)
        {
            RuleFor(v => v.email).NotEmpty().WithMessage("Missing fields").WithErrorCode("422").EmailAddress().WithMessage("Invalid fields").WithErrorCode("422");
            RuleFor(v => v.firstName).NotEmpty().WithMessage("Missing fields").WithErrorCode("422");
            RuleFor(v => v.lastName).NotEmpty().WithMessage("Missing fields").WithErrorCode("422");
            RuleFor(v => v.phones).NotNull().WithMessage("Missing fields").WithErrorCode("422").NotEmpty().WithMessage("Missing fields").WithErrorCode("422");
            RuleForEach(v => v.phones).SetValidator(validator);
        }
    }
}
