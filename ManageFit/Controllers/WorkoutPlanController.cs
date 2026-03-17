using Contracts.Requests.WorkoutPlan;
using Microsoft.AspNetCore.Mvc;
using Contracts.Responses.WorkoutPlan;
using MediatR;
using Entities.WorkoutPlan.Model;
using Entities.Common;
using Services.Commands.WorkoutPlan;
using Services.Queries.WorkoutPlan;
using Contracts.Responses.Client;

namespace ManageFit.Controllers
{
    [Route("api/workout-plans")]
    [ApiController]
    public class WorkoutPlanController(IMediator mediator) : ControllerBase
    {
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<WorkoutPlanController>/5
        [HttpGet("trainer/{trainerId}")]
        public async Task<IEnumerable<GetWorkoutPlanByTrainerIdResponse>> GetForTrainer(Guid trainerId)
        {
            Result<IEnumerable<WorkoutPlan>> result = await mediator.Send(new GetWorkoutPlansForTrainerQuery(trainerId: trainerId));
            IEnumerable<GetWorkoutPlanByTrainerIdResponse> results = result.Value.Select(x => new GetWorkoutPlanByTrainerIdResponse(
                name: x.Name,
                id: x.Id,
                exercises: x.WorkoutPlanExercise.Select(y => new GetWorkoutPlanExerciseResponse(name: y.Exercises.Name,
                                                                                                description: y.Exercises.Description,
                                                                                                videoUrl: y.Exercises.VideoUrl,
                                                                                                id: y.Exercises.Id,
                                                                                                set: y.Set,
                                                                                                repetition: y.Repetition,
                                                                                                time: y.Time)),
                clients: x.WorkoutPlanClient.Select(y => new GetClientResponse(name: y.Clients.Name,
                                                                               weight: y.Clients.Weight,
                                                                               height: y.Clients.Height,
                                                                               email: y.Clients.Email,
                                                                               id: y.Clients.Id))));
            return results;
        }

        [HttpPost]
        public async Task<AddWorkoutPlanResponse>AddWorkoutPlan([FromBody] AddWorkoutPlanRequest request)
        {
            Result<WorkoutPlan> wokroutPlanResult = await mediator.Send(new AddWorkoutPlanCommand(
                name: request.Name,
                clientIds: request.ClientUids,
                trainerId: request.TrainerUid,
                workoutPlanExercises: request.exercises));

            return new AddWorkoutPlanResponse();
        }

        [HttpPut("{workoutPlanId}")]
        public async Task<AddWorkoutPlanResponse> PutAsync(Guid workoutPlanId, [FromBody] EditWorkoutPlanRequest request)
        {
            Result<WorkoutPlan> wokroutPlanResult = await mediator.Send(new EditWorkoutPlanCommand(
                name: request.Name,
                clientIds: request.ClientUids,
                trainerId: request.TrainerUid,
                workoutPlanExercises: request.exercises,
                workoutPlanId: workoutPlanId));

            return new AddWorkoutPlanResponse();
        }

        [HttpDelete("{workoutPlanId}")]
        public async Task<DeleteWorkoutPlanResponse>Delete(Guid workoutPlanId)
        {
            Result<WorkoutPlan> result = await mediator.Send(new DeleteWorkoutPlanExerciseCommand(workoutPlanId));

            return new DeleteWorkoutPlanResponse(name: result.Value.Name);
        }
    }
}
