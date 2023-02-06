using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;
using TechKartApplication.Models;

namespace com.TechKart.UserApi.Models
{
    [ExcludeFromCodeCoverage]
    public class Cart
    {
        [Key]
        public int CartId { get; set; }
        [Required]
        public double GrandTotal { get; set; }
        [Required]
        public DateTime Created_at { get; set; }
        [Required]
        public DateTime Modified_at { get; set; }

        //Navigation
        [JsonIgnore]
        public LoginDetail LoginDetail { get; set; }
        [ForeignKey("LoginDetail")]
        public int LoginId { get; set; }
        [JsonIgnore]
        public List<CartItems> cartItems { get; set; }
    }
}