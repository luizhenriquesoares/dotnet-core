using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Web.Api.Modules.Auth {

    public class PhoneDto {
        public int number { get; set; }
        public int area_code { get; set; }
        public string country_code { get; set; }
    }
}