using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Web.Api.Modules.Auth.Dtos {

    public class SigninDto {
        public string email { get; set; }
        public string password { get; set; }
    }
}