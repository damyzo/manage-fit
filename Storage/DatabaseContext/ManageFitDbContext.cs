using Microsoft.EntityFrameworkCore;


namespace Storage.DatabaseContext
{
    public partial class ManageFitDbContext(DbContextOptions<ManageFitDbContext> options) : DbContext(options)
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }

}
