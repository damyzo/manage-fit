namespace Services.Commands.WorkoutPlan
{
    using Contracts.Requests.WorkoutPlan;
    using Entities.Common;
    using Entities.Exercise.Model;
    using Entities.WorkoutPlan.Model;
    using Entities.WorkoutPlanClient.Model;
    using Entities.WorkoutPlanExercise.Model;
    using MediatR;
    using Storage.Repositories.Client.Interface;
    using Storage.Repositories.Exercise.Interface;
    using Storage.Repositories.Trainer.Interface;
    using Storage.Repositories.WorkoutPlan.Interface;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class EditWorkoutPlanCommand(
        string name,
        IEnumerable<Guid> clientIds,
        Guid trainerId,
        Guid workoutPlanId,
        IEnumerable<EditWorkoutPlanExerciseRequest> workoutPlanExercises) : IRequest<Result<WorkoutPlan>>
    {
        public Guid WorkoutPlanId { get; set; } = workoutPlanId;
        public string Name { get; set; } = name;

        public IEnumerable<Guid> ClientIds { get; set; } = clientIds;

        public Guid TrainerId { get; set; } = trainerId;

        public IEnumerable<EditWorkoutPlanExerciseRequest> workoutPlanExercises { get; set; } = workoutPlanExercises;
    }

    public class EditWorkoutPlanCommandHandler(
        IWorkoutPlanRepository workoutPlanRepository,
        ITrainerRepository trainerRepository,
        IClientRepository clientRepository,
        IExerciseRepository exerciseRepository) : IRequestHandler<EditWorkoutPlanCommand, Result<WorkoutPlan>>
    {
        public async Task<Result<WorkoutPlan>> Handle(EditWorkoutPlanCommand request, CancellationToken cancellationToken)
        {
            Result<WorkoutPlan> workoutPlan = await workoutPlanRepository.GetWorkoutPlan(request.WorkoutPlanId, cancellationToken);

            if (!workoutPlan.IsSuccess)
            {
                return workoutPlan;
            }

            if (!await trainerRepository.CheckIfTrainerExists(request.TrainerId, cancellationToken))
            {
                return new Result<WorkoutPlan>(
                        value: new WorkoutPlan() { Name = string.Empty },
                        isSuccess: false,
                        message: $"Trainer with ID {request.TrainerId} does not exist."
                    );
            }

            if (!await clientRepository.CheckIfClientsExist(request.ClientIds, cancellationToken) && request.ClientIds.Any())
            {
                new Result<WorkoutPlan>(
                        value: new WorkoutPlan() { Name = string.Empty },
                        isSuccess: false,
                        message: "Cannot add a workout plan to a user that does not exits."
                    );
            }

            List<WorkoutPlanExercise> workoutPlanExercises = [];
            IEnumerable<Exercise> exercisesToAdd = [];

            foreach (EditWorkoutPlanExerciseRequest exercise in request.workoutPlanExercises)
            {
                if (exercise.Id != null)
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

                    exercisesToAdd = exercisesToAdd.Append(new Exercise
                    {
                        Id = exerciseUid,
                        Description = exercise.Description,
                        Name = exercise.Name,
                        TrainerId = request.TrainerId,
                        VideoUrl = exercise.VideoUrl,
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

                if (!exercisesResult.IsSuccess)
                {
                    return new Result<WorkoutPlan>(
                            value: new WorkoutPlan() { Name = string.Empty },
                            isSuccess: false,
                            message: exercisesResult.Message
                        );
                }
            }

            workoutPlan.Value.Name = request.Name;
            workoutPlan.Value.TrainerId = request.TrainerId;
            workoutPlan.Value.WorkoutPlanExercise = workoutPlanExercises;
            workoutPlan.Value.WorkoutPlanClient = request.ClientIds.Select(x => new WorkoutPlanClient { ClientsId = x }).ToList();

            Result<WorkoutPlan> workoutPlanResult = await workoutPlanRepository.UpdateWorkoutPlan(workoutPlan.Value, cancellationToken);

            return workoutPlanResult;
        }
    }
}
