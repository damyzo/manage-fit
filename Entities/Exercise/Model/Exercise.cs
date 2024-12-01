namespace Entities.Exercise.Model
{
    using Entities.Common;
    using Entities.Trainer.Model;
    using Entities.WorkoutPlanExercise.Model;

    public class Exercise : Entity
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public string VideoUrl { get; set; }

        public Guid TrainerId { get; set; }
        
        public Trainer Trainer { get; set; } = null!;
        
        public List<WorkoutPlanExercise> WorkoutPlanExercises { get; } = [];
    }
}
