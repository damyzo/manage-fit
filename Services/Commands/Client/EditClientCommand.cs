namespace Services.Commands.Client
{
    using Entities.Client.Model;
    using MediatR;
    using Entities.Common;
    using Storage.Repositories.Client.Interface;

    public class EditClientCommand(
        string name,
        float weight,
        float height,
        string email,
        Guid uid) : IRequest<Result<Client>>
    {
        public string Name { get; set; } = name;

        public float Weight { get; set; } = weight;

        public float Height { get; set; } = height;

        public string Email { get; set; } = email;
        
        public Guid Uid { get; set; } = uid;
    }

    public class EditClientCommandHandler(IClientRepository clientRepository) : IRequestHandler<EditClientCommand, Result<Client>>
    {
        public async Task<Result<Client>> Handle(EditClientCommand request, CancellationToken cancellationToken)
        {
            Client client = new()
            {
                Name = request.Name,
                Weight = request.Weight,
                Height = request.Height,
                Email = request.Email,
                Uid = request.Uid
            };

            Result<Client> clientResult = Validators.ClientValidator.Validate(client);

            if (!clientResult.IsSuccess)
            {
                return await Task.FromResult(clientResult);
            }

            clientResult = await clientRepository.UpdateClient(client, cancellationToken);

            return clientResult;
        }
    }
}
