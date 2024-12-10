namespace Entities.Meal.Model
{
    using Entities.Common;
    using Entities.Ingredient.Model;

    public class Meal : Entity
    {
        string Name { get; set; }

        public string Description { get; set; }

        public IEnumerable<Ingredient> Ingredients { get; set; } = [];
    }
}
