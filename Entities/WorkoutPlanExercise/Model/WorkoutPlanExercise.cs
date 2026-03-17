namespace Entities.WorkoutPlanExercise.Model
{
    using Entities.Common;
    using Entities.Exercise.Model;
    using Entities.WorkoutPlan.Model;

    public class WorkoutPlanExercise
    {
        public Guid WorkoutPlansId { get; set; }
        
        public Guid ExercisesId { get; set; }

        public virtual WorkoutPlan WorkoutPlans { get; set; } = null!;

        public virtual Exercise Exercises { get; set; } = null!;

        public int Set {  get; set; }

        public int Repetition { get; set; }

        public double Time { get; set; }
    }
}
