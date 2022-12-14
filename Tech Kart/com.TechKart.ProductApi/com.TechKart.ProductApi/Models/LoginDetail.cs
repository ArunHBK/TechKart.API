using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace com.TechKart.ProductApi.Models
{
    public class LoginDetail
    {
        [Key]
        public int LoginId { get; set; }
        [Required]
        public string Username { get; set; } = string.Empty;
        [Required]
        public string Password { get; set; } = string.Empty;
        [Required]
        public string Role { get; set; } = string.Empty;

        //Navigation
        [JsonIgnore]
        public Cart cart { get; set; }
    }
}
