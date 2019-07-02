using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Web.Api.Modules.Auth.Dtos {
    public class SignupDto {
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public ICollection<PhoneDto> phones { get; set; }
    }

}