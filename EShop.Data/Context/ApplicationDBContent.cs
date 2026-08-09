using System.Linq;
using EShop.Data.Entities.Account;
using EShop.Data.Entities.OrderEntities;
using EShop.Data.Entities.ProductEntities;
using Microsoft.EntityFrameworkCore;
namespace EShop.Data.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {


        }
        #region Account
        public DbSet<User> Users { get; set; }

        #endregion

        #region Products
        public DbSet<Product> Products { get; set; }
        public DbSet<Brand> brands { get; set; }
        public DbSet<ProductSelectedBrand> productSelectedBrands { get; set; }
        public DbSet<ProductCategory> productCategories { get; set; }
        public DbSet<ProductColor> productColors { get; set; }
        public DbSet<ProductComment> ProductComments { get; set; }
        public DbSet<ProductFeature> ProductFeatures { get; set; }
        public DbSet<ProductGallery> productGalleries { get; set; }
        public DbSet<ProductSelectedCategory> productSelectedCategories { get; set; }
        public DbSet<ProductVariant> productVariants { get; set; }
        #endregion

        #region Order
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<PaymentRecord> PaymentRecord { get; set; }
        #endregion
        #region FilterData
        //یسری قوانین تعیین میشه
        //مثلا مواردی که حذف چند تایی هستند cascade مثلا هرجا که از اون محصول استفاده میشده اونا حذف میشه
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var cascadeFKs = modelBuilder.Model.GetEntityTypes()
                .SelectMany(t => t.GetForeignKeys())
                .Where(fk => !fk.IsOwnership && fk.DeleteBehavior == DeleteBehavior.Cascade);

            // اما وقتی میخواهیم فقط اون حذف بشه و سفارشات و بقیه موارد نه از این خط کد استفاده میکنیم restrict
            foreach (var fk in cascadeFKs)
                fk.DeleteBehavior = DeleteBehavior.Restrict;

            //داده هایی و حذف کن که حذف نشده باشند
            modelBuilder.Entity<User>()
                .HasQueryFilter(u => !u.IsDeleted);
            modelBuilder.Entity<Product>()
                .HasQueryFilter(u => !u.IsDeleted);
            modelBuilder.Entity<ProductCategory>()
                .HasQueryFilter(u => !u.IsDeleted);
            modelBuilder.Entity<ProductColor>()
               .HasQueryFilter(u => !u.IsDeleted);
            modelBuilder.Entity<ProductComment>()
               .HasQueryFilter(u => !u.IsDeleted);
            modelBuilder.Entity<ProductFeature>()
               .HasQueryFilter(u => !u.IsDeleted);
            modelBuilder.Entity<ProductSelectedCategory>()
               .HasQueryFilter(u => !u.IsDeleted);
            modelBuilder.Entity<Brand>()
               .HasQueryFilter(u => !u.IsDeleted);
            modelBuilder.Entity<ProductSelectedBrand>()
               .HasQueryFilter(u => !u.IsDeleted);
            modelBuilder.Entity<ProductGallery>()
               .HasQueryFilter(u => !u.IsDeleted);
            modelBuilder.Entity<Order>()
                .HasQueryFilter(u => !u.IsDeleted);
            modelBuilder.Entity<OrderDetail>()
                .HasQueryFilter(u => !u.IsDeleted);
            modelBuilder.Entity<PaymentRecord>()
               .HasQueryFilter(u => !u.IsDeleted);

        }

        #endregion

     

    }
}
