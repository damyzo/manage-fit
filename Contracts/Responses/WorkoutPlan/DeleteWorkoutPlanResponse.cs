namespace Contracts.Responses.WorkoutPlan
{
    public class DeleteWorkoutPlanResponse(string name)
    {
        public string Name { get; } = name;
    }
}
