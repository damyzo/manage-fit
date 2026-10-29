namespace Storage.DatabaseContext
{
    using Entities.Client.Model;
    using Entities.Exercise.Model;
    using Entities.Gym.Model;
    using Entities.Ingredient.Model;
    using Entities.Meal.Model;
    using Entities.MealIngredient.Model;
    using Entities.Trainer.Model;
    using Entities.WorkoutPlan.Model;
    using Entities.WorkoutPlanClient.Model;
    using Entities.WorkoutPlanExercise.Model;
    using Microsoft.EntityFrameworkCore;

    public partial class ManageFitDbContext(DbContextOptions<ManageFitDbContext> options) : DbContext(options), IDisposable
    {
        public DbSet<Client> Client { get; set; }

        public DbSet<Trainer> Trainer { get; set; }

        public DbSet<Gym> Gym { get; set; }

        public DbSet<Exercise> Exercise { get; set; }

        public DbSet<WorkoutPlan> WorkoutPlan { get; set; }

        public DbSet<WorkoutPlanExercise> WorkoutPlanExercise { get; set; }

        public DbSet<WorkoutPlanClient> WorkoutPlanClient { get; set; }

        public DbSet<Meal> Meal { get; set; }

        public DbSet<Ingredient> Ingredient { get; set; }

        public DbSet<MealIngredient> MealIngredient { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<WorkoutPlanExercise>()
                .HasKey(x => new { x.WorkoutPlansId, x.ExercisesId, x.Order });

            modelBuilder.Entity<WorkoutPlanClient>()
                .HasKey(x => new { x.ClientsId, x.WorkoutPlansId });
        }
    }

}
