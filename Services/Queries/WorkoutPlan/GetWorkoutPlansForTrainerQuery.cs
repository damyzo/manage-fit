namespace Services.Queries.WorkoutPlan
{
    using Entities.Common;
    using Entities.WorkoutPlan.Model;
    using MediatR;
    using Storage.Repositories.WorkoutPlan.Interface;
    using Storage.Repositories.Trainer.Interface;
    using Entities.Trainer.Model;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class GetWorkoutPlansForTrainerQuery(string trainerEmail) : IRequest<Result<IEnumerable<WorkoutPlan>>>
    {
        public string TrainerEmail { get; } = trainerEmail;
    }

    public class GetWorkoutPlansForTrainerQueryHandler(
        IWorkoutPlanRepository workoutPlanRepository,
        ITrainerRepository trainerRepository) : IRequestHandler<GetWorkoutPlansForTrainerQuery, Result<IEnumerable<WorkoutPlan>>>
    {
        public async Task<Result<IEnumerable<WorkoutPlan>>> Handle(GetWorkoutPlansForTrainerQuery request, CancellationToken cancellationToken)
        {
            Result<Trainer> trainerResult = await trainerRepository.GetTrainerByEmail(request.TrainerEmail, cancellationToken);

            if (!trainerResult.IsSuccess)
            {
                return new Result<IEnumerable<WorkoutPlan>>(value: [], isSuccess: false, message: trainerResult.Message);
            }

            Result<IEnumerable<WorkoutPlan>> result = await workoutPlanRepository.GetWorkoutPlansByTrainerId(trainerResult.Value.Id, cancellationToken);

            return result;
        }
    }
}
