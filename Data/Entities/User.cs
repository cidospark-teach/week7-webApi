using Data.Enums;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities
{
    public class User : IdentityUser
    {
        [Required]
        [StringLength(50, MinimumLength =3, ErrorMessage ="Length must be between 3 - 50 characters")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Length must be between 3 - 50 characters")]
        public string LastName { get; set; }

        [Required]
        public string AttendanceStatus { get; set; }
    }
}
