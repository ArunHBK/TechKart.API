using com.TechKart.CartApi.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace com.TechKart.CartApi.Data
{
    public class DataContext: DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }
        public DbSet<LoginDetail> LoginDetails { get; set; }
        public DbSet<UserDetail> UserDetails { get; set; }
        public DbSet<ProductDetail> ProductDetails { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartItems> CartItems { get; set; }
    }
}
