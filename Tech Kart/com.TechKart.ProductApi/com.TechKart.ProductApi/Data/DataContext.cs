using com.TechKart.ProductApi.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace com.TechKart.ProductApi.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }
        public DbSet<ProductDetail> ProductDetails { get; set; }
    }
}
