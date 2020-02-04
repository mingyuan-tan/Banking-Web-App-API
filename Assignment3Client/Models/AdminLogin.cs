using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Assignment3Client.Models
{
    public class AdminLogin
    {
        [Required(ErrorMessage = "Login ID required") ]
        public string AdminLoginID { get; set; } = "admin";
        [Required(ErrorMessage = "Password Required")]
        public string AdminPassword { get; } = "admin";
    }
}
