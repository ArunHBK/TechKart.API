using com.TechKart.UserApi.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace TechKartApplication.Models
{
    [ExcludeFromCodeCoverage]
    public class ProductDetail
    {
        [Key]
        public int ProductId { get; set; }
        [Required]
        public string Name { get; set; } = string.Empty;
        [Required]
        public double Price { get; set; }
        [Required]
        public string Brand { get; set; } = string.Empty;
        [Required]
        public string Category { get; set; } = string.Empty;
        [Required]
        public string Description { get; set; } = string.Empty;
        [Required]
        public string ImgUrl { get; set; } = string.Empty;

        //Navigation
        [JsonIgnore]
        public List<CartItems>  cartItems { get; set; }
    }
}