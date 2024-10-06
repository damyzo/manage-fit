using Contracts.Requests.Client;
using Entities.Client.Model;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Services.Commands.Client;
using Entities.Common;
using Services.Queries.Client;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ManageFit.Controllers
{
    [Route("api/clients")]
    [ApiController]
    public class ClientController(
        IMediator mediator) : ControllerBase
    {
        // GET: api/<ValuesController>
        [HttpGet]
        public async Task<Result<IEnumerable<Client>>> GetAsync()
        {
            Result<IEnumerable<Client>> result = await mediator.Send(request: new GetClientsQuery());

            return result;
        }

        // GET api/<ValuesController>/5
        [HttpGet("{uid}")]
        public async Task<Result<Client>> Get(Guid uid)
        {
            Result<Client> reuslt = await mediator.Send(request: new GetClientQuery(clientUid: uid));

            return reuslt;
        }

        // POST api/<ValuesController>
        [HttpPost]
        public async Task<Result<Client>> Post([FromBody] AddClientRequest client)
        {
            Result<Client> result = await mediator.Send(request: new AddClientCommand(
                name: client.Name,
                weight: client.Weight,
                height: client.Height,
                email: client.Email));

            return result;
        }

        // PUT api/<ValuesController>/5
        [HttpPut("{uid}")]
        public async Task<Result<Client>> Put(Guid uid, [FromBody] EditClientRequest client)
        {
            Result<Client> result = await mediator.Send(request: new EditClientCommand(
                name: client.Name,
                weight: client.Weight,
                height: client.Height,
                email: client.Email,
                uid: uid));

            return result;
        }

        // DELETE api/<ValuesController>/5
        [HttpDelete("{uid}")]
        public void Delete(Guid uid)
        {

        }
    }
}
