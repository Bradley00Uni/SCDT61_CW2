using Microsoft.EntityFrameworkCore;
using OnlineShopDeliveryAPI.Models;

namespace OnlineShopDeliveryAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<OrderModel> Orders {get; set;}
        public DbSet<ProductModel> Products {get; set;}
        public DbSet<DeliveryModel> Deliveries {get; set;}
        public DbSet<UserModel> Users { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
