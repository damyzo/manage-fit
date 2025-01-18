namespace Services.Commands.Client
{
    using Entities.Client.Model;
    using MediatR;
    using Entities.Common;
    using Storage.Repositories.Client.Interface;
    using Storage.Repositories.Trainer.Interface;
    using Entities.Trainer.Model;

    public class AddClientCommand(
        string name,
        float weight,
        float height,
        string email,
        Guid trainerId) : IRequest<Result<Client>>
    {
        public string Name { get; set; } = name;

        public float Weight { get; set; } = weight;

        public float Height { get; set; } = height;

        public string Email { get; set; } = email;

        public Guid TrainerId { get; set; } = trainerId;
    }

    public class AddClientCommandHandler(
        IClientRepository clientRepository,
        ITrainerRepository trainerRepository) : IRequestHandler<AddClientCommand, Result<Client>>
    {
        public async Task<Result<Client>> Handle(AddClientCommand request, CancellationToken cancellationToken)
        {
            Result<Trainer> trainerResult = await trainerRepository.GetTrainer(request.TrainerId, cancellationToken);

            if (!trainerResult.IsSuccess)
            {
                return new Result<Client>(
                    value: new Client() { Name = "", Email = "", Height = 0, Weight = 0, Id = Guid.Empty},
                    isSuccess: false,
                    message: trainerResult.Message); ;
            }

            Client client = new()
            {
                Name = request.Name,
                Weight = request.Weight,
                Height = request.Height,
                Email = request.Email,
                Id = Guid.NewGuid()
            };

            client.TrainerClients.Add(
                new Entities.TrainerClient.Model.TrainerClient() { 
                    Client = client,
                    Trainer = trainerResult.Value,
                    ClientId = client.Id,
                    TrainerId = trainerResult.Value.Id });

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
