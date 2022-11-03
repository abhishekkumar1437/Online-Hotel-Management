using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyAppModels
{
    public class ApplicationUser : IdentityUser
    {   [Required]

        public string Name { get; set; }   
        public string? PhoneNumber { get; set; }
        public string? city { get; set; }
        public string? state { get; set; }
        public string? pincode { get; set; }
       
    }
}
