using Microsoft.AspNetCore.Identity;

namespace Web.Api.Modules.Auth.Domain {
    public class Phone {
        public int PhoneId { get; set; }
        public int Number { get; set; }
        public int Area_code { get; set; }
        public string Country_code { get; set; }

    }
}