namespace Contracts.Responses.WorkoutPlan
{
    using Contracts.Responses.Client;

    public class GetWorkoutPlanByTrainerIdResponse(
        string name,
        Guid id,
        IEnumerable<GetWorkoutPlanExerciseResponse> exercises,
        IEnumerable<GetClientResponse> clients)
    {
        public string Name { get; } = name;
        
        public string Id { get; } = id.ToString();

        public IEnumerable<GetWorkoutPlanExerciseResponse> Exercises { get; } = exercises;

        public IEnumerable<GetClientResponse> Clients { get; } = clients;
    }
}
