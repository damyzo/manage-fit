namespace Entities.WorkoutPlan.Model
{
    using Entities.Common;
    using Entities.Trainer.Model;
    using Entities.WorkoutPlanExercise.Model;
    using Entities.WorkoutPlanClient.Model;
    using Entities.Exercise.Model;

    public class WorkoutPlan : Entity
    {
        public required string Name { get; set; }

        public Guid TrainerId { get; set; }

        public virtual Trainer Trainer { get; set; } = null!;

        public virtual ICollection<WorkoutPlanExercise> WorkoutPlanExercise { get; set; } = [];

        public virtual ICollection<WorkoutPlanClient> WorkoutPlanClients { get; set; } = [];

    }
}
