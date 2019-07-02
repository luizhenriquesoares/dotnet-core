using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace Web.Api.Modules.Auth.Domain {
    public class User : IdentityUser<long> {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public virtual DateTime Last_Login { get; set; }
        public virtual DateTime Created_At { get; set; }
        public ICollection<Phone> Phones { get; set; }
    }
}