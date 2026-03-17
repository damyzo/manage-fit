namespace Contracts.Responses.WorkoutPlan
{
    public class GetWorkoutPlanExerciseResponse(
        string name,
        string description,
        string videoUrl,
        Guid id,
        int set,
        int repetition,
        double time)
    {
        public string Name { get; } = name;

        public string Description { get; } = description;

        public string VideoUrl { get; } = videoUrl;

        public int Set { get; set; } = set;

        public int Repetition { get; set; } = repetition;

        public double Time { get; set; } = time;

        public string Id { get; } = id.ToString();
    }
}
