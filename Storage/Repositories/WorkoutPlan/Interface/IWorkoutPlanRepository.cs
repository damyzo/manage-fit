namespace Storage.Repositories.WorkoutPlan.Interface
{
    using Entities.Common;
    using Entities.WorkoutPlan.Model;

    public interface IWorkoutPlanRepository
    {
        public Task<Result<WorkoutPlan>> GetWorkoutPlan(
            Guid workoutPlanId,
            CancellationToken cancellationToken);

        public Task<Result<IEnumerable<WorkoutPlan>>> GetWorkoutPlansByTrainerIdAndClientId(
            Guid trainerId,
            Guid clientId,
            CancellationToken cancellationToken);

        public Task<Result<WorkoutPlan>> AddWorkoutPlan(
            WorkoutPlan workoutPlan,
            CancellationToken cancellationToken);

        public Task<Result<WorkoutPlan>> UpdateWorkoutPlan(
            WorkoutPlan workoutPlan,
            CancellationToken cancellationToken);

        public Task<Result<WorkoutPlan>> DeleteWorkoutPlan(
            Guid workoutPlanId,
            CancellationToken cancellationToken);
    }
}
