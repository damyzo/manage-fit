using Contracts.Requests.WorkoutPlan;
using Microsoft.AspNetCore.Mvc;
using Contracts.Responses.WorkoutPlan;
using MediatR;
using Entities.WorkoutPlan.Model;
using Entities.Common;
using Services.Commands.WorkoutPlan;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ManageFit.Controllers
{
    [Route("api/workout-plans")]
    [ApiController]
    public class WorkoutPlanController(IMediator mediator) : ControllerBase
    {
        // GET: api/<WorkoutPlanController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<WorkoutPlanController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<WorkoutPlanController>
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

        // PUT api/<WorkoutPlanController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<WorkoutPlanController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
