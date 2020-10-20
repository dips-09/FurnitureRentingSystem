using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AuthenticationAPI.Model
{
    public class RentingUser
    {
        [Key]
        public string UserId { get; set; }
        [Required(ErrorMessage = "Enter a password")]
        public string Password { get; set; }
        public string FullName { get; set; }
    }
}
