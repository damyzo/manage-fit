using Contracts.Requests;
using Entities.Client.Model;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Services.Commands.Client;
using Services.Common;
using Services.Queries.Client;
using Storage.DatabaseContext;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ManageFit.Controllers
{
    [Route("api/[controller]")]
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
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ValuesController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
