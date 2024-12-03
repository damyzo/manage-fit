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

        public List<WorkoutPlanExercise> WorkoutPlanExercises { get; } = [];

        public Trainer Trainer { get; set; } = null!;

        public Client Client { get; set; } = null!;
    }
}
