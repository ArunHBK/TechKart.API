using Microsoft.VisualBasic;
using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace com.TechKart.ProductApi.Models
{
    public class CartItems
    {
        [Key]
        public int CartItemId { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required]
        public double Item_Total { get; set; }
        [Required]
        public DateTime Created_at { get; set; }
        [Required]
        public DateTime Modified_at { get; set; }

        //Navigation
        [Required]
        public int CartId { get; set; }
        [JsonIgnore]
        public Cart cart { get; set; }
        [Required]
        public int ProductId { get; set; }
        [JsonIgnore]
        public ProductDetail product { get; set; }
    }
}
