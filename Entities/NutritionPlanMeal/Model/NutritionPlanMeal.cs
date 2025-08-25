namespace Entities.NutritionPlanMeal.Model
{
    using Entities.Common;
    using Entities.Meal.Model;
    using Entities.NutritionPlan.Model;

    public class NutritionPlanMeal : Entity
    {    
        public Guid MealId { get; set; }

        public virtual Meal Meal { get; set; } = null!;

        public Guid NutritionPlanId { get; set; }

        public virtual NutritionPlan NutritionPlan { get; set; } = null!;
    }
}
