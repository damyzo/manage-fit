namespace Entities.WorkoutPlan.Model
{
    using Entities.Client.Model;
    using Entities.Common;
    using Entities.Trainer.Model;
    using Entities.WorkoutPlanExercise.Model;

    public class WorkoutPlan : Entity
    {
        public Guid TrainerId { get; set; }

        public Guid ClientId { get; set; }

        public virtual ICollection<WorkoutPlanExercise> WorkoutPlanExercises { get; } = [];

        public virtual Trainer Trainer { get; set; } = null!;

        public virtual Client Client { get; set; } = null!;
    }
}
