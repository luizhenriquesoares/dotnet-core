using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Web.Api.Modules.Auth.Dtos {
    public class UserDto {
        public string firstName { get; set; }
        public string lastName { get; set; }
        public List<PhoneDto> phones { get; set; }

        [EmailAddress]
        public string email { get; set; }
        public virtual DateTime created_at { get; set; }
        public virtual DateTime last_login { get; set; }
    }
}