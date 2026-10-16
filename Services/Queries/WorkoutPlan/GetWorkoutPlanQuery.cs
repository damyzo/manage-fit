namespace Services.Queries.WorkoutPlan
{
    using Entities.Common;
    using Entities.WorkoutPlan.Model;
    using MediatR;
    using Storage.Repositories.WorkoutPlan.Interface;
    using System;
    using System.Threading.Tasks;

    public class GetWorkoutPlanQuery(Guid workoutPlanId) : IRequest<Result<WorkoutPlan>>
    {
        public Guid WorkoutPlanId { get; } = workoutPlanId;
    }

    public class GetWorkoutPlanQueryHandler(IWorkoutPlanRepository workoutPlanRepository) : IRequestHandler<GetWorkoutPlanQuery, Result<WorkoutPlan>>
    {
        public async Task<Result<WorkoutPlan>> Handle(GetWorkoutPlanQuery request, CancellationToken cancellationToken)
        {
            Result<WorkoutPlan> result = await workoutPlanRepository.GetWorkoutPlanWithDetails(request.WorkoutPlanId, cancellationToken);

            return result;
        }
    }
}
