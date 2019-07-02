using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Api.Modules.Auth;

namespace Web.Api.src.Modules.Auth.Validators
{
    public class PhoneDtoValidator : AbstractValidator<PhoneDto>
    {
        public PhoneDtoValidator()
        {
            RuleFor(v => v.area_code).GreaterThan(0).WithMessage("Missing fields").WithErrorCode("422");
            RuleFor(v => v.country_code).NotEmpty().WithMessage("Missing fields").WithErrorCode("422");
            RuleFor(v => v.number).GreaterThan(0).WithMessage("Missing fields").WithErrorCode("422");
        }
    }
}
