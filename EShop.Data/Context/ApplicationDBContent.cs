using Microsoft.EntityFrameworkCore;
namespace EShop.Data.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<DbContext> options) : base(options)
        {


        }
    }
}
