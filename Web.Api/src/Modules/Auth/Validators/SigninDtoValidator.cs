using FluentValidation;
using Web.Api.Modules.Auth.Dtos;

namespace Web.Api.Modules.Auth.Validators
{
    public class SigninDtoValidator : AbstractValidator<SigninDto>
    {
        public SigninDtoValidator()
        {
            RuleFor(v => v.email).NotEmpty().WithMessage("Missing fields").WithErrorCode("422").EmailAddress().WithMessage("Invalid fields").WithErrorCode("422");
            RuleFor(v => v.password).NotEmpty().WithMessage("Missing fields").WithErrorCode("422");
        }
    }

}