using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UdemyFormation.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; }

        [ValidateNever]
        public string Role { get; set; }
    }
}