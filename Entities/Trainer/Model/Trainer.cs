namespace Entities.Trainer.Model
{
    using Entities.Client.Model;
    using Entities.Common;
    using Entities.Exercise.Model;
    using Entities.Ingredient.Model;
    using Entities.Meal.Model;
    using Entities.WorkoutPlan.Model;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Trainer() : Entity
    {
        public required string Name { get; set; }

        public required string Email { get; set; }

        [Column("ClientId")]
        public IEnumerable<Client> Clients { get; set; } = new List<Client>();

        public IEnumerable<Exercise> Exercises { get; } = new List<Exercise>();

        public IEnumerable<WorkoutPlan> Workouts { get; set; } = new List<WorkoutPlan>();

        public IEnumerable<Ingredient> Ingredients { get; set; } = new List<Ingredient>();

        public IEnumerable<Meal> Meals { get; set; } = new List<Meal>();
    }
}
