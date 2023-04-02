using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class AddUserDto
    {
        [Required]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Length must be between 3 - 50 characters")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Length must be between 3 - 50 characters")]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string AttendanceStatus { get; set; }
    }
}