namespace Services.Commands.Client
{
    using Entities.Client.Model;
    using MediatR;
    using Entities.Common;
    using Storage.Repositories.Client.Interface;

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

    public class AddClientCommandHandler(IClientRepository clientRepository) : IRequestHandler<AddClientCommand, Result<Client>>
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

            clientResult = await clientRepository.AddClient(client, cancellationToken);

            return clientResult;
        }
    }
}
