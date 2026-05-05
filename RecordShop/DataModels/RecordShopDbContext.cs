using Microsoft.EntityFrameworkCore;
using RecordShop.DataModels;

namespace RecordShop.DataModels
{
    public class RecordShopDbContext : DbContext
    {
        public DbSet<Album> Albums { get; set; }
        public RecordShopDbContext(DbContextOptions<RecordShopDbContext> options) : base (options)
        {
        
        }
    }
}
