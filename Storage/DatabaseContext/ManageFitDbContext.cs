using Entities.Client.Model;
using Microsoft.EntityFrameworkCore;
using Storage.Configuration;


namespace Storage.DatabaseContext
{
    public partial class ManageFitDbContext : DbContext, IDisposable
    {
        public ManageFitDbContext(DbContextOptions<ManageFitDbContext> options) 
            : base(options) { }
        public DbSet<Client> Client { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new ClientModelConfiguration());
        }
    }

}
