using Contracts.Requests.Client;
using Entities.Client.Model;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Services.Commands.Client;
using Entities.Common;
using Services.Queries.Client;
using Contracts.Responses.Client;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ManageFit.Controllers
{
    [ApiController]
    [Route("api/clients")]
    public class ClientController(
        IMediator mediator) : ControllerBase
    {
        // GET: api/<ValuesController>
        [HttpGet("trainer/{trainerId}")]
        public async Task<IEnumerable<GetClientsResponse>> GetClients(Guid trainerId)
        {
            Result<IEnumerable<Client>> result = await mediator.Send(request: 
                new GetClientsQuery(
                    trainerId: trainerId));

            return result.Value.Select(x => 
                new GetClientsResponse(
                    name: x.Name,
                    weight: x.Weight,
                    height: x.Height,
                    email: x.Email,
                    id: x.Id));;
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
                id: result.Value.Id);
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
                trainerId: client.TrainerId));

            return new AddClientResponse(
                name: result.Value.Name,
                weight: result.Value.Weight,
                height: result.Value.Height,
                email: result.Value.Email);
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
                id: id));

            return new EditClientResponse(
                name: result.Value.Name,
                weight: result.Value.Weight,
                height: result.Value.Height,
                email: result.Value.Email);
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
                email: result.Value.Email);
        }
    }
}
