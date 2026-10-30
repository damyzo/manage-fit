using Contracts.Requests.Client;
using Entities.Client.Model;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Services.Commands.Client;
using Entities.Common;
using Services.Queries.Client;
using Contracts.Responses.Client;
using Microsoft.AspNetCore.Authorization;
using ManageFit.Extensions;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ManageFit.Controllers
{
    [ApiController]
    [Route("api/clients")]
    public class ClientController(
        IMediator mediator) : ControllerBase
    {
        //[Authorize]
        // GET: api/<ValuesController>
        [HttpGet("trainer")]
        public async Task<IEnumerable<GetClientsResponse>> GetClients()
        {
            Result<IEnumerable<Client>> result = await mediator.Send(request: 
                new GetClientsQuery(
                    trainerEmail: User.GetUserEmail()));

            return result.Value.Select(x => 
                new GetClientsResponse(
                    name: x.Name,
                    weight: x.Weight,
                    height: x.Height,
                    email: x.Email,
                    dateOfBirth: x.DateOfBirth,
                    id: x.Id,
                    gymId: x.GymId,
                    gymLocations: x.ClientGymLocations.Select(l => new Contracts.Responses.Gym.GymLocationResponse(l.GymLocation.Id, l.GymLocation.Address)).ToList()));
        }

        // GET api/<ValuesController>/5
        [HttpGet("{id}")]
        public async Task<GetClientResponse> GetClient(Guid id)
        {
            Result<Client> result = await mediator.Send(request: new GetClientQuery(clientId: id));

            return new GetClientResponse(
                name: result.Value.Name,
                weight: result.Value.Weight,
                height: result.Value.Height,
                email: result.Value.Email,
                dateOfBirth: result.Value.DateOfBirth,
                id: result.Value.Id,
                gymId: result.Value.GymId,
                gymLocations: result.Value.ClientGymLocations.Select(l => new Contracts.Responses.Gym.GymLocationResponse(l.GymLocation.Id, l.GymLocation.Address)).ToList());
        }

        // POST api/<ValuesController>
        [HttpPost]
        public async Task<AddClientResponse> AddClient([FromBody] AddClientRequest client)
        {
            Result<Client> result = await mediator.Send(request: new AddClientCommand(
                name: client.Name,
                weight: client.Weight,
                height: client.Height,
                email: client.Email,
                dateOfBirth: client.DateOfBirth,
                trainerEmail: User.GetUserEmail(),
                gymLocationIds: client.GymLocationIds));

            return new AddClientResponse(
                name: result.Value.Name,
                weight: result.Value.Weight,
                height: result.Value.Height,
                email: result.Value.Email,
                dateOfBirth: result.Value.DateOfBirth);
        }

        // PUT api/<ValuesController>/5
        [HttpPut("{id}")]
        public async Task<EditClientResponse> EditClient(Guid id, [FromBody] EditClientRequest client)
        {
            Result<Client> result = await mediator.Send(request: new EditClientCommand(
                name: client.Name,
                weight: client.Weight,
                height: client.Height,
                email: client.Email,
                dateOfBirth: client.DateOfBirth,
                id: id,
                gymLocationIds: client.GymLocationIds));

            return new EditClientResponse(
                name: result.Value.Name,
                weight: result.Value.Weight,
                height: result.Value.Height,
                email: result.Value.Email,
                dateOfBirth: result.Value.DateOfBirth);
        }

        // DELETE api/<ValuesController>/5
        [HttpDelete("{id}")]
        public async Task<DeleteClientResponse> DeleteClient(Guid id)
        {
            Result<Client> result = await mediator.Send(request: new DeleteClientCommand(id: id));

            return new DeleteClientResponse(
                name: result.Value.Name,
                weight: result.Value.Weight,
                height: result.Value.Height,
                email: result.Value.Email,
                dateOfBirth: result.Value.DateOfBirth);
        }
    }
}
