using System.ComponentModel.DataAnnotations;

namespace TechKartApplication.Models.DTO
{
    public class UserDetailDto
    {
        [Required]
        public string FirstName { get; set; } = string.Empty;
        [Required]
        public string LastName { get; set; } = string.Empty;
        [Required]
        public int Age { get; set; }
        [Required]
        public string Gender { get; set; } = string.Empty;
        [Required]
        public string Username { get; set; } = string.Empty;
        [Required]
        public string Password { get; set; }
        [Required]
        public string Address { get; set; } = string.Empty;
        [Required]
        public long ContactNumber { get; set; }
    }
}