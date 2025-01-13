using Contracts.Requests.Trainer;
using Contracts.Responses.Trainer;
using Entities.Common;
using Entities.Trainer.Model;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Services.Commands.Trainer;

namespace ManageFit.Controllers
{
    [Route("api/trainers")]
    [ApiController]
    public class TrainerController(IMediator mediator) : ControllerBase
    {
        [HttpPost]
        public async Task<AddTrainerResponse> AddClient([FromBody] AddTrainerRequest request)
        {
            Result<Trainer> trainerResult =
                await mediator.Send(request: new AddTrainerCommand(
                    name: request.Name,
                    email: request.Email));

            return new AddTrainerResponse(
                name: trainerResult.Value.Name,
                email: trainerResult.Value.Email);
        }
    }
}
