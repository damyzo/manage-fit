namespace Services.Commands.WorkoutPlan
{
    using Entities.Common;
    using Entities.WorkoutPlan.Model;
    using MediatR;
    using Storage.Repositories.WorkoutPlan.Interface;
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    public class DeleteWorkoutPlanExerciseCommand(Guid id) : IRequest<Result<WorkoutPlan>>
    {
        public Guid Id { get; set; } = id;
    }

    public class DeleteWorkoutPlanExerciseCommandHandler(IWorkoutPlanRepository workoutPlanRepository) : IRequestHandler<DeleteWorkoutPlanExerciseCommand, Result<WorkoutPlan>>
    {
        public async Task<Result<WorkoutPlan>> Handle(DeleteWorkoutPlanExerciseCommand request, CancellationToken cancellationToken)
        {
            Result<WorkoutPlan> result = await workoutPlanRepository.DeleteWorkoutPlan(request.Id, cancellationToken);
            
            return result;
        }
    }
}
