namespace Entities.Trainer.Model
{
    using Entities.Client.Model;
    using Entities.Common;
    using Entities.Exercise.Model;
    using Entities.Ingredient.Model;
    using Entities.Meal.Model;
    using Entities.TrainerClient.Model;
    using Entities.WorkoutPlan.Model;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Trainer() : Entity
    {
        public required string Name { get; set; }

        public required string Email { get; set; }

        public List<TrainerClient> TrainerClients { get; set; } = [];

        public List<Exercise> Exercises { get; } = [];

        public List<WorkoutPlan> Workouts { get; set; } = [];

        public List<Ingredient> Ingredients { get; set; } = [];

        public List<Meal> Meals { get; set; } = [];
    }
}
