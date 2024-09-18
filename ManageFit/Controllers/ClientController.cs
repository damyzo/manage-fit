using Contracts.Requests;
using Entities.Client.Model;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Services.Commands;
using Services.Common;
using Storage.DatabaseContext;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ManageFit.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController(
        ManageFitDbContext manageFitDbContext,
        IMediator mediator) : ControllerBase
    {
        // GET: api/<ValuesController>
        [HttpGet]
        public async Task<IEnumerable<string>> GetAsync()
        {
            Client client = new Client { Email = "asd", Uid = Guid.NewGuid(), Height = 12, Weight = 13, Name = "asd" };
            manageFitDbContext.Client.Add(client);
            await manageFitDbContext.SaveChangesAsync();


            return new List<string>() { "asdasd" };
        }

        // GET api/<ValuesController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<ValuesController>
        [HttpPost]
        public async Task<Result<Client>> Post([FromBody] AddClientRequest value)
        {
            Result<Client> result = await mediator.Send(request: new AddClientCommand(
                name: value.Name,
                weight: value.Weight,
                height: value.Height,
                email: value.Email));

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
