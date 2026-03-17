namespace Services.Commands.WorkoutPlan
{
    using Entities.Common;
    using MediatR;
    using Entities.WorkoutPlan.Model;
    using System.Threading.Tasks;
    using System.Threading;
    using Storage.Repositories.WorkoutPlan.Interface;
    using Contracts.Requests.WorkoutPlan;
    using Storage.Repositories.Trainer.Interface;
    using Storage.Repositories.Client.Interface;
    using Storage.Repositories.Exercise.Interface;
    using Entities.Exercise.Model;
    using Entities.WorkoutPlanExercise.Model;
    using Entities.WorkoutPlanClient.Model;

    public class AddWorkoutPlanCommand(
        string name,
        IEnumerable<Guid> clientIds,
        Guid trainerId,
        IEnumerable<AddWorkoutPlanExerciseRequest> workoutPlanExercises) : IRequest<Result<WorkoutPlan>>
    {
        public string Name { get; set; } = name;

        public IEnumerable<Guid> ClientIds { get; set; } = clientIds;

        public Guid TrainerId { get; set; } = trainerId;

        public IEnumerable<AddWorkoutPlanExerciseRequest> workoutPlanExercises { get; set; } =  workoutPlanExercises;
    }

    public class AddWorkoutPlanCommandHandler(
        IWorkoutPlanRepository workoutPlanRepository,
        ITrainerRepository trainerRepository,
        IClientRepository clientRepository,
        IExerciseRepository exerciseRepository) : IRequestHandler<AddWorkoutPlanCommand, Result<WorkoutPlan>>
    {
        public async Task<Result<WorkoutPlan>> Handle(AddWorkoutPlanCommand request, CancellationToken cancellationToken)
        {
            if (!await trainerRepository.CheckIfTrainerExists(request.TrainerId, cancellationToken))
            {
                return new Result<WorkoutPlan>(
                        value: new WorkoutPlan() { Name = string.Empty },
                        isSuccess: false,
                        message: $"Trainer with ID {request.TrainerId} does not exist."
                    );
            }

            if(!await clientRepository.CheckIfClientsExist(request.ClientIds, cancellationToken) && request.ClientIds.Any())
            {
                new Result<WorkoutPlan>(
                        value: new WorkoutPlan() { Name = string.Empty },
                        isSuccess: false,
                        message: "Cannot add a workout plan to a user that does not exits."
                    );
            }

            IEnumerable<Guid> exerciseUids = [];
            IEnumerable<Exercise> exercisesToAdd = [];

            foreach (AddWorkoutPlanExerciseRequest exercise in request.workoutPlanExercises)
            {
                if(exercise.Id != null)
                {
                    exerciseUids = exerciseUids.Append((Guid)exercise.Id);
                }
                else
                {
                    Guid exerciseUid = Guid.NewGuid();

                    exercisesToAdd = exercisesToAdd.Append(new Exercise {
                        Id = exerciseUid,
                        Description = exercise.Description,
                        Name = exercise.Name,
                        TrainerId = request.TrainerId,
                        VideoUrl = exercise.VideoUrl,
                    });
                    exerciseUids = exerciseUids.Append(exerciseUid);
                }
            }

            if (exercisesToAdd.Any())
            {
                Result<IEnumerable<Exercise>> exercisesResult = await exerciseRepository.AddExercises(exercisesToAdd, cancellationToken);

                if(!exercisesResult.IsSuccess)
                {
                    return new Result<WorkoutPlan>(
                            value: new WorkoutPlan() { Name = string.Empty },
                            isSuccess: false,
                            message: exercisesResult.Message
                        );
                }
            }

            WorkoutPlan workoutPlan = new WorkoutPlan() {
                Name = request.Name,
                TrainerId = request.TrainerId,
                WorkoutPlanExercise = exerciseUids.Select(x => new WorkoutPlanExercise() { ExercisesId = x }).ToList(),
                WorkoutPlanClient = request.ClientIds.Select(x => new WorkoutPlanClient { ClientsId = x }).ToList(),
            };

            Result<WorkoutPlan> workoutPlanResult = await workoutPlanRepository.AddWorkoutPlan(workoutPlan, cancellationToken);

            return workoutPlanResult;
        }
    }

}
