﻿using com.TechKart.UserApi.Models;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace TechKartApplication.Models
{
    public class UserDetail
    {
        [Key]
        public int Id { get; set; }
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
        public string Address { get; set; } = string.Empty;
        [Required]
        public long ContactNumber { get; set; }
       
    }
}
