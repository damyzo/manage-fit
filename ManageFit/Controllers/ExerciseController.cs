using Contracts.Responses.Exercise;
using Entities.Common;
using Entities.Exercise.Model;
using Microsoft.AspNetCore.Mvc;
using Storage.Repositories.Exercise.Interface;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ManageFit.Controllers
{
    [ApiController]
    [Route("api/exercises")]
    public class ExerciseController(IExerciseRepository exerciseRepository) : ControllerBase
    {
        // GET: api/<ExerciseController>
        [HttpGet]
        public IEnumerable<GetExercisesResponse> GetExercises()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<ExerciseController>/5
        [HttpGet("{id}")]
        public async Task<Exercise> GetExercise(Guid id)
        {
            Result<Exercise> exercise = await exerciseRepository.GetExercise(id, default);
            
            return exercise.Value;
        }

        // POST api/<ExerciseController>
        [HttpPost]
        public async Task AddExercise()
        {
            await exerciseRepository.AddExercise(new Exercise() { Description = "Test", Name = "Test", VideoUrl = "Test", Id = Guid.NewGuid() }, default);
        }

        // PUT api/<ExerciseController>/5
        [HttpPut("{id}")]
        public void EditExercise(Guid id, [FromBody] Exercise exercise)
        {
        }

        // DELETE api/<ExerciseController>/5
        [HttpDelete("{id}")]
        public void DeleteExercise(Guid id)
        {
        }
    }
}
