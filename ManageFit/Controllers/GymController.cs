using Contracts.Requests.Gym;
using Contracts.Responses.Gym;
using Entities.Common;
using Entities.Gym.Model;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Services.Commands.Gym;
using Services.Queries.Gym;

namespace ManageFit.Controllers
{
    [ApiController]
    [Route("api/gyms")]
    public class GymController(
        IMediator mediator) : ControllerBase
    {
        [HttpGet]
        public async Task<IEnumerable<GetGymsResponse>> GetGyms()
        {
            Result<IEnumerable<Gym>> result = await mediator.Send(request: new GetGymsQuery());

            return result.Value.Select(x =>
                new GetGymsResponse(
                    name: x.Name,
                    location: x.Location,
                    id: x.Id));
        }

        [HttpGet("{id}")]
        public async Task<GetGymResponse> GetGym(Guid id)
        {
            Result<Gym> result = await mediator.Send(request: new GetGymQuery(gymId: id));

            return new GetGymResponse(
                name: result.Value.Name,
                location: result.Value.Location,
                id: result.Value.Id);
        }

        [HttpPost]
        public async Task<AddGymResponse> AddGym([FromBody] AddGymRequest gym)
        {
            Result<Gym> result = await mediator.Send(request: new AddGymCommand(
                name: gym.Name,
                location: gym.Location));

            return new AddGymResponse(
                name: result.Value.Name,
                location: result.Value.Location);
        }

        [HttpPut("{id}")]
        public async Task<EditGymResponse> EditGym(Guid id, [FromBody] EditGymRequest gym)
        {
            Result<Gym> result = await mediator.Send(request: new EditGymCommand(
                name: gym.Name,
                location: gym.Location,
                id: id));

            return new EditGymResponse(
                name: result.Value.Name,
                location: result.Value.Location);
        }

        [HttpDelete("{id}")]
        public async Task<DeleteGymResponse> DeleteGym(Guid id)
        {
            Result<Gym> result = await mediator.Send(request: new DeleteGymCommand(id: id));

            return new DeleteGymResponse(
                name: result.Value.Name,
                location: result.Value.Location);
        }
    }
}
