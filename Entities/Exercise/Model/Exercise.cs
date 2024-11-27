namespace Entities.Exercise.Model
{
    using Entities.Common;
    using Entities.WorkoutPlanExercise.Model;

    public class Exercise : Entity
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public string VideoUrl { get; set; }

        public List<WorkoutPlanExercise> WorkoutPlanExercises { get; } = [];
    }
}
