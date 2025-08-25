namespace Entities.Trainer.Model
{
    using Entities.Common;
    using Entities.Exercise.Model;
    using Entities.Ingredient.Model;
    using Entities.Meal.Model;
    using Entities.TrainerClient.Model;
    using Entities.WorkoutPlan.Model;

    public class Trainer : Entity
    {
        public required string Name { get; set; }

        public required string Email { get; set; }

        public virtual ICollection<TrainerClient> TrainerClients { get; set; } = [];

        public virtual ICollection<Exercise> Exercises { get; } = [];

        public virtual ICollection<WorkoutPlan> Workouts { get; set; } = [];

        public virtual ICollection<Ingredient> Ingredients { get; set; } = [];

        public virtual ICollection<Meal> Meals { get; set; } = [];
    }
}
