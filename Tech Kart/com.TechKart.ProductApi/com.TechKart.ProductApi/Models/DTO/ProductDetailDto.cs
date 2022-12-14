using System.ComponentModel.DataAnnotations;

namespace com.TechKart.ProductApi.Models.DTO
{
    public class ProductDetailDto
    {
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
    }
}
