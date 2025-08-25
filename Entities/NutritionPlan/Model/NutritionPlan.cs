namespace Entities.NutritionPlan.Model
{
    using Entities.Client.Model;
    using Entities.Common;
    using Entities.NutritionPlanMeal.Model;
    using Entities.Trainer.Model;

    public class NutritionPlan : Entity
    {
        public required string Name { get; set; }

        public required string Description { get; set; }

        public Guid ClientId { get; set; }

        public virtual Client Client { get; set; } = null!;

        public Guid TrainerId { get; set; }

        public virtual Trainer Trainer { get; set; } = null!;

        public virtual ICollection<NutritionPlanMeal> NutritionPlanMeal { get; set; } = [];
    }
}
