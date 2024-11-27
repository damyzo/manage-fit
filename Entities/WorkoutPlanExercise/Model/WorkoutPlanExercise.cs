namespace Entities.WorkoutPlanExercise.Model
{
    using Entities.Common;
    using Entities.Exercise.Model;
    using Entities.WorkoutPlan.Model;

    public class WorkoutPlanExercise : Entity
    {
        public Guid WorkoutPlansId { get; set; }
        
        public Guid ExercisesId { get; set; }

        public WorkoutPlan WorkoutPlan { get; set; } = null!;

        public Exercise Exercise { get; set; } = null!;

        public int Set {  get; set; }

        public int Repetition { get; set; }

        public float Time { get; set; }
    }
}
