﻿using Contracts.Requests.Client;
using Entities.Client.Model;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Services.Commands.Client;
using Entities.Common;
using Services.Queries.Client;
using Microsoft.AspNetCore.Authorization;
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
        [HttpGet("trainer/{trainerGuid}")]
        public async Task<IEnumerable<GetClientsResponse>> GetClients(Guid trainerGuid)
        {
            Result<IEnumerable<Client>> result = await mediator.Send(request: 
                new GetClientsQuery(
                    trainerGuid: trainerGuid));

            return result.Value.Select(x => 
                new GetClientsResponse(
                    name: x.Name,
                    weight: x.Weight,
                    height: x.Height,
                    email: x.Email,
                    uid: x.Uid));;
        }

        // GET api/<ValuesController>/5
        [HttpGet("{uid}")]
        public async Task<GetClientsResponse> GetClient(Guid uid)
        {
            Result<Client> result = await mediator.Send(request: new GetClientQuery(clientUid: uid));

            return new GetClientsResponse(
                name: result.Value.Name,
                weight: result.Value.Weight,
                height: result.Value.Height,
                email: result.Value.Email,
                uid: result.Value.Uid);
        }

        // POST api/<ValuesController>
        [HttpPost]
        public async Task<Result<Client>> AddClient([FromBody] AddClientRequest client)
        {
            Result<Client> result = await mediator.Send(request: new AddClientCommand(
                name: client.Name,
                weight: client.Weight,
                height: client.Height,
                email: client.Email,
                trainerGuid: client.TrainerGuid));

            return result;
        }

        // PUT api/<ValuesController>/5
        [HttpPut("{uid}")]
        public async Task<Result<Client>> EditClient(Guid uid, [FromBody] EditClientRequest client)
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
        public async Task<Result<Client>> DeleteClient(Guid uid)
        {
            Result<Client> result = await mediator.Send(request: new DeleteClientCommand(uid: uid));

            return result;
        }
    }
}
