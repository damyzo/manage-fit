namespace Entities.Meal.Model
{
    using Entities.Common;
    using Entities.MealIngredient.Model;
    using Entities.NutritionPlanMeal.Model;
    using Entities.Trainer.Model;

    public class Meal : Entity
    {
        public required string Name { get; set; }

        public required string Description { get; set; }

        public Guid TrainerId { get; set; }

        public virtual Trainer Trainer { get; set; } = null!;

        public virtual ICollection<MealIngredient> MealIngredients { get; set; } = [];

        public virtual ICollection<NutritionPlanMeal> NutritionPlanMeal { get; set;} = [];
    }
}
