namespace Storage.DatabaseContext
{
    using Entities.Client.Model;
    using Entities.Exercise.Model;
    using Entities.Ingredient.Model;
    using Entities.Meal.Model;
    using Entities.MealIngredient.Model;
    using Entities.Trainer.Model;
    using Entities.WorkoutPlan.Model;
    using Entities.WorkoutPlanExercise.Model;
    using Microsoft.EntityFrameworkCore;

    public partial class ManageFitDbContext(DbContextOptions<ManageFitDbContext> options) : DbContext(options), IDisposable
    {
        public DbSet<Client> Client { get; set; }

        public DbSet<Trainer> Trainer { get; set; }

        public DbSet<Exercise> Exercise { get; set; }

        public DbSet<WorkoutPlan> WorkoutPlan { get; set; }

        public DbSet<WorkoutPlanExercise> WorkoutPlanExercises { get; set; }

        public DbSet<Meal> Meal { get; set; }

        public DbSet<Ingredient> Ingredient { get; set; }

        public DbSet<MealIngredient> MealIngredient { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }

}
