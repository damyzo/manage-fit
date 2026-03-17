namespace Services.Queries.WorkoutPlan
{
    using Entities.Common;
    using Entities.WorkoutPlan.Model;
    using MediatR;
    using Storage.Repositories.WorkoutPlan.Interface;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class GetWorkoutPlansForTrainerQuery(Guid trainerId) : IRequest<Result<IEnumerable<WorkoutPlan>>>
    {
        public Guid TrainerId { get; } = trainerId;
    }

    public class GetWorkoutPlansForTrainerQueryHandler(IWorkoutPlanRepository workoutPlanRepository) : IRequestHandler<GetWorkoutPlansForTrainerQuery, Result<IEnumerable<WorkoutPlan>>>
    {
        public async Task<Result<IEnumerable<WorkoutPlan>>> Handle(GetWorkoutPlansForTrainerQuery request, CancellationToken cancellationToken)
        {
            Result<IEnumerable<WorkoutPlan>> result = await workoutPlanRepository.GetWorkoutPlansByTrainerId(request.TrainerId, cancellationToken);

            return result;
        }
    }
}
