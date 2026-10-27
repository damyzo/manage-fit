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
    using Entities.Trainer.Model;
    using Entities.WorkoutPlanExercise.Model;
    using Entities.WorkoutPlanClient.Model;

    public class AddWorkoutPlanCommand(
        string name,
        IEnumerable<Guid> clientIds,
        string trainerEmail,
        IEnumerable<AddWorkoutPlanExerciseRequest> workoutPlanExercises) : IRequest<Result<WorkoutPlan>>
    {
        public string Name { get; set; } = name;

        public IEnumerable<Guid> ClientIds { get; set; } = clientIds;

        public string TrainerEmail { get; set; } = trainerEmail;

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
            Result<Trainer> trainerResult = await trainerRepository.GetTrainerByEmail(request.TrainerEmail, cancellationToken);

            if (!trainerResult.IsSuccess)
            {
                return new Result<WorkoutPlan>(
                        value: new WorkoutPlan() { Name = string.Empty },
                        isSuccess: false,
                        message: $"Trainer with email {request.TrainerEmail} does not exist."
                    );
            }

            Guid trainerId = trainerResult.Value.Id;

            if(!await clientRepository.CheckIfClientsExist(request.ClientIds, cancellationToken) && request.ClientIds.Any())
            {
                new Result<WorkoutPlan>(
                        value: new WorkoutPlan() { Name = string.Empty },
                        isSuccess: false,
                        message: "Cannot add a workout plan to a user that does not exits."
                    );
            }

            List<WorkoutPlanExercise> workoutPlanExercises = [];
            IEnumerable<Exercise> exercisesToAdd = [];

            foreach (AddWorkoutPlanExerciseRequest exercise in request.workoutPlanExercises)
            {
                if(exercise.Id != null)
                {
                    workoutPlanExercises.Add(new WorkoutPlanExercise()
                    {
                        ExercisesId = (Guid)exercise.Id,
                        Order = exercise.Order,
                        Set = exercise.Set,
                        Repetition = exercise.Repetition,
                        Time = exercise.Time,
                    });
                }
                else
                {
                    Guid exerciseUid = Guid.NewGuid();

                    exercisesToAdd = exercisesToAdd.Append(new Exercise {
                        Id = exerciseUid,
                        Description = exercise.Description,
                        Name = exercise.Name,
                        TrainerId = trainerId,
                        VideoUrl = exercise.VideoUrl
                    });
                    workoutPlanExercises.Add(new WorkoutPlanExercise()
                    {
                        ExercisesId = exerciseUid,
                        Order = exercise.Order,
                        Set = exercise.Set,
                        Repetition = exercise.Repetition,
                        Time = exercise.Time,
                    });
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
                TrainerId = trainerId,
                WorkoutPlanExercise = workoutPlanExercises,
                WorkoutPlanClient = request.ClientIds.Select(x => new WorkoutPlanClient { ClientsId = x }).ToList(),
            };

            Result<WorkoutPlan> workoutPlanResult = await workoutPlanRepository.AddWorkoutPlan(workoutPlan, cancellationToken);

            return workoutPlanResult;
        }
    }

}
