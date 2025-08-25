namespace Entities.Exercise.Model
{
    using Entities.Common;
    using Entities.Trainer.Model;
    using Entities.WorkoutPlanExercise.Model;

    public class Exercise : Entity
    {
        public required string Name { get; set; }

        public required string Description { get; set; }

        public required string VideoUrl { get; set; }

        public Guid TrainerId { get; set; }
        
        public virtual Trainer Trainer { get; set; } = null!;

        public virtual ICollection<WorkoutPlanExercise> WorkoutPlanExercises { get; } = [];
    }
}
