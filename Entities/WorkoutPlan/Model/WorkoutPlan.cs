namespace Entities.WorkoutPlan.Model
{
    using Entities.Common;
    using Entities.WorkoutPlanExercise.Model;

    public class WorkoutPlan : Entity
    {
        public List<WorkoutPlanExercise> WorkoutPlanExercises { get; } = [];
    }
}
