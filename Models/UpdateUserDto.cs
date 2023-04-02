using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class UpdateUserDto
    {
        [Required]
        public string Id { get; set; }
        [Required]
        public string AttendanceStatus { get; set; }
    }
}
