namespace Entities.Meal.Model
{
    using Entities.Common;
    using Entities.Ingredient.Model;
    using Entities.NutritionPlanMeal.Model;
    using Entities.Trainer.Model;

    public class Meal : Entity
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public Guid TrainerId { get; set; }

        public Trainer Trainer { get; set; } = null!;

        public List<Ingredient> Ingredients { get; set; } = [];

        public List<NutritionPlanMeal> NutritionPlanMeal { get; set;} = [];
    }
}
