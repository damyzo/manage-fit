namespace Entities.NutritionPlanMeal.Model
{
    using Entities.Common;
    using Entities.Meal.Model;
    using Entities.NutritionPlan.Model;

    public class NutritionPlanMeal : Entity
    {    
        public Guid MealId { get; set; }

        public Meal Meal { get; set; } = null!;

        public Guid NutritionPlanId { get; set; }

        public NutritionPlan NutritionPlan { get; set; } = null!;
    }
}
