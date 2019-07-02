using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Api.Modules.Auth.Domain
{
    public class ApplicationUserRole : IdentityUserRole<long>
    {
        private Guid Id { get; set; }
        public virtual User User { get; set; }
        public virtual ApplicationRole Role { get; set; }
    }
}
