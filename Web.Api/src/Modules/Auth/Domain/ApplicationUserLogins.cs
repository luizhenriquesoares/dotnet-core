using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Api.Modules.Auth.Domain
{
    public class ApplicationUserLogin : IdentityUserLogin<long>
    {
        public virtual User User { get; set; }
    }
}
