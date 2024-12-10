namespace Entities.Ingredient.Model
{
    using Entities.Common;
    using Entities.Meal.Model;

    public class Ingredient : Entity
    {
        public string Name { get; set; }

        public float Calories { get; set; }

        public float Protein { get; set; }

        public float Fat { get; set; }

        public float Carbohydrate { get; set; }

        public IEnumerable<Meal> Meals { get; set; } = [];
    }
}
