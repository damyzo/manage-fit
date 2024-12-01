namespace ManageFit.Controllers
{
    using Entities.Common;
    using Contracts.Responses.Exercise;
    using Entities.Exercise.Model;
    using Microsoft.AspNetCore.Mvc;
    using Contracts.Requests.Exercise;
    using MediatR;
    using Services.Commands.Exercise;
    using Services.Queries.Exercise;

    [ApiController]
    [Route("api/exercises")]
    public class ExerciseController(IMediator mediator) : ControllerBase
    {
        // GET: api/<ExerciseController>
        [HttpGet("trainer/{trainerId}")]
        public async Task<IEnumerable<GetExercisesResponse>> GetExercises(Guid trainerId)
        {
            Result<IEnumerable<Exercise>> result = await mediator.Send(new GetExercisesQuery(trainerId));

            return result.Value.Select(x => new GetExercisesResponse(
                name: x.Name,
                description: x.Description,
                videoUrl: x.VideoUrl,
                id: x.Id));
        }

        // GET api/<ExerciseController>/5
        [HttpGet("{id}")]
        public async Task<GetExerciseResponse> GetExercise(Guid id)
        {
            Result<Exercise> exercise = await mediator.Send(new GetExerciseQuery(id));
            
            return new GetExerciseResponse(
                name: exercise.Value.Name,
                description: exercise.Value.Description,
                videoUrl: exercise.Value.VideoUrl,
                id: exercise.Value.Id);
        }

        // POST api/<ExerciseController>
        [HttpPost]
        public async Task<AddExerciseResponse> AddExercise([FromBody] AddExerciseRequest exerciseRequest)
        {
            Result<Exercise> exercise = await mediator.Send(new AddExerciseCommand(
                description: exerciseRequest.Description,
                name: exerciseRequest.Name,
                videoUrl: exerciseRequest.VideoUrl, 
                trainerId: exerciseRequest.TrainerId
            ), default);

            return new AddExerciseResponse(
                name: exercise.Value.Name,
                description: exercise.Value.Description,
                videoUrl: exercise.Value.VideoUrl,
                id: exercise.Value.Id);
        }

        // PUT api/<ExerciseController>/5
        [HttpPut("{id}")]
        public async Task<EditExerciseResponse> EditExercise(Guid id, [FromBody] EditExerciseRequest exerciseRequest)
        {
            Result<Exercise> exercise = await mediator.Send(request: new EditExerciseCommand(
                name: exerciseRequest.Name,
                description: exerciseRequest.Description,
                videoUrl: exerciseRequest.VideoUrl,
                exerciseId: id));

            return new EditExerciseResponse(
                name: exercise.Value.Name,
                description: exercise.Value.Description,
                videoUrl: exercise.Value.VideoUrl,
                id: exercise.Value.Id);
        }

        // DELETE api/<ExerciseController>/5
        [HttpDelete("{id}")]
        public async Task<DeleteExerciseResponse> DeleteExerciseAsync(Guid id)
        {
            Result<Exercise> result = await mediator.Send(new DeleteExerciseCommand(exerciseId: id), default);

            return new DeleteExerciseResponse(
                name: result.Value.Name,
                description: result.Value.Description,
                videoUrl: result.Value.VideoUrl,
                id: result.Value.Id);
        }
    }
}
