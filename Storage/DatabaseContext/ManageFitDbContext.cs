namespace Storage.DatabaseContext
{
    using Entities.Client.Model;
    using Entities.Exercise.Model;
    using Entities.Trainer.Model;
    using Entities.WorkoutPlan.Model;
    using Entities.WorkoutPlanExercise.Model;
    using Microsoft.EntityFrameworkCore;
    using Storage.Configuration;

    public partial class ManageFitDbContext : DbContext, IDisposable
    {
        public ManageFitDbContext(DbContextOptions<ManageFitDbContext> options) 
            : base(options) { }

        public DbSet<Client> Client { get; set; }

        public DbSet<Trainer> Trainer { get; set; }

        public DbSet<Exercise> Exercise { get; set; }

        public DbSet<WorkoutPlan> WorkoutPlan { get; set; }

        public DbSet<WorkoutPlanExercise> WorkoutPlanExercises { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new ClientModelConfiguration());
            modelBuilder.ApplyConfiguration(new TrainerModelConfiguration());
        }
    }

}
