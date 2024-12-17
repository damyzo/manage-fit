namespace Entities.MealIngredient.Model
{
    using Entities.Ingredient.Model;
    using Entities.Meal.Model;

    public class MealIngredient
    {
        public Guid MealsId { get; set; }

        public Meal Meal { get; set; } = null!;

        public Guid IngredientsId { get; set; }

        public Ingredient Ingredient { get; set; } = null!;

        public float Grams { get; set; }

        public float Liters { get; set; }
    }
}
