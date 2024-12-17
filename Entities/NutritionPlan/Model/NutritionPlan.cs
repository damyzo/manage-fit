namespace Entities.NutritionPlan.Model
{
    using Entities.Client.Model;
    using Entities.Common;
    using Entities.Meal.Model;
    using Entities.NutritionPlanMeal.Model;
    using Entities.Trainer.Model;

    public class NutritionPlan : Entity
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public Guid ClientId { get; set; }

        public Client Client { get; set; } = null!;

        public Guid TrainerId { get; set; }

        public Trainer Trainer { get; set; } = null!;

        public IEnumerable<NutritionPlanMeal> NutritionPlanMeal { get; set; } = [];
    }
}
