using System.Linq;
using EShop.Data.Entities.Account;
using Microsoft.EntityFrameworkCore;
namespace EShop.Data.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {


        }
        #region Account
       // public DbSet<User> Users { get; set; }


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
            //modelBuilder.Entity<User>()
            //    .HasQueryFilter(u => !u.IsDeleted);
            
                
        }

        #endregion

    }
}
