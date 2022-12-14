using com.TechKart.UserApi.Models;
using Microsoft.EntityFrameworkCore;
using TechKartApplication.Models;

namespace TechKartApplication.Data
{
    public class DataContext : DbContext
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
