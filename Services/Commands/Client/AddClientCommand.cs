namespace Services.Commands.Client
{
    using Entities.Client.Model;
    using MediatR;
    using Entities.Common;
    using Storage.Repositories.Client.Interface;
    using Storage.Repositories.Trainer.Interface;
    using Entities.Trainer.Model;
    using Entities.Gym.Model;
    using Entities.ClientGymLocation.Model;
    using Storage.Repositories.Gym.Interface;

    public class AddClientCommand(
        string name,
        float weight,
        float height,
        string email,
        string trainerEmail,
        IEnumerable<Guid>? gymLocationIds = null) : IRequest<Result<Client>>
    {
        public string Name { get; set; } = name;

        public float Weight { get; set; } = weight;

        public float Height { get; set; } = height;

        public string Email { get; set; } = email;

        public string TrainerEmail { get; set; } = trainerEmail;

        public IEnumerable<Guid> GymLocationIds { get; set; } = gymLocationIds ?? [];
    }

    public class AddClientCommandHandler(
        IClientRepository clientRepository,
        ITrainerRepository trainerRepository,
        IGymRepository gymRepository) : IRequestHandler<AddClientCommand, Result<Client>>
    {
        public async Task<Result<Client>> Handle(AddClientCommand request, CancellationToken cancellationToken)
        {
            Result<Trainer> trainerResult = await trainerRepository.GetTrainerByEmail(request.TrainerEmail, cancellationToken);

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
                GymId = trainerResult.Value.GymId,
                Id = Guid.NewGuid()
            };

            List<Guid> locationIds = request.GymLocationIds.Distinct().ToList();

            if (locationIds.Count > 0)
            {
                string? error = null;
                Gym? gym = null;

                if (trainerResult.Value.GymId == null)
                {
                    error = "Trainer that is not part of a gym cannot assign gym locations.";
                }
                else
                {
                    Result<Gym> gymResult = await gymRepository.GetGym(trainerResult.Value.GymId.Value, cancellationToken);

                    if (!gymResult.IsSuccess)
                    {
                        error = gymResult.Message;
                    }
                    else
                    {
                        gym = gymResult.Value;

                        if (locationIds.Any(id => !gym.Locations.Any(l => l.Id == id)))
                        {
                            error = "All gym locations should belong to the trainer's gym.";
                        }
                    }
                }

                if (error != null)
                {
                    return new Result<Client>(
                        value: new Client() { Name = "", Email = "", Height = 0, Weight = 0, Id = Guid.Empty },
                        isSuccess: false,
                        message: error);
                }

                foreach (Guid locationId in locationIds)
                {
                    client.ClientGymLocations.Add(new ClientGymLocation()
                    {
                        Id = Guid.NewGuid(),
                        ClientId = client.Id,
                        GymLocationId = locationId
                    });
                }
            }

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
