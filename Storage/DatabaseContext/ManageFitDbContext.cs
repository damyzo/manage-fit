namespace Storage.DatabaseContext
{
    using Entities.Client.Model;
    using Entities.Trainer.Model;
    using Microsoft.EntityFrameworkCore;
    using Storage.Configuration;

    public partial class ManageFitDbContext : DbContext, IDisposable
    {
        public ManageFitDbContext(DbContextOptions<ManageFitDbContext> options) 
            : base(options) { }
        public DbSet<Client> Client { get; set; }
        public DbSet<Trainer> Trainer { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new ClientModelConfiguration());
            modelBuilder.ApplyConfiguration(new TrainerModelConfiguration());
        }
    }

}
