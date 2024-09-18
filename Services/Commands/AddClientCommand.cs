using Entities.Client.Model;
using MediatR;
using Services.Common;
using Storage.DatabaseContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Commands
{
    public class AddClientCommand(
        string name,
        float weight,
        float height,
        string email) : IRequest<Result<Client>>
    {
        public string Name { get; set; } = name;

        public float Weight { get; set; } = weight;

        public float Height { get; set; } = height;

        public string Email { get; set; } = email;
    }

    public class AddClientCommandHandler(ManageFitDbContext manageFitDbContext) : IRequestHandler<AddClientCommand, Result<Client>>
    {
        public async Task<Result<Client>> Handle(AddClientCommand request, CancellationToken cancellationToken)
        {
            Client client = new()
            {
                Name = request.Name,
                Weight = request.Weight,
                Height = request.Height,
                Email = request.Email,
                Uid = Guid.NewGuid()
            };

            Result<Client> clientResult = Validators.ClientValidator.Validate(client);

            if (!clientResult.IsSuccess)
            {
                return await Task.FromResult(clientResult);
            }

            manageFitDbContext.Client.Add(clientResult.Value);

            await manageFitDbContext.SaveChangesAsync(cancellationToken);

            return clientResult;
        }
    }
}
