namespace Services.Commands.Client
{
    using Entities.Client.Model;
    using MediatR;
    using Entities.Common;
    using Storage.Repositories.Client.Interface;
    using Entities.Gym.Model;
    using Entities.ClientGymLocation.Model;
    using Storage.Repositories.Gym.Interface;

    public class EditClientCommand(
        string name,
        float weight,
        float height,
        string email,
        Guid id,
        IEnumerable<Guid>? gymLocationIds = null) : IRequest<Result<Client>>
    {
        public string Name { get; set; } = name;

        public float Weight { get; set; } = weight;

        public float Height { get; set; } = height;

        public string Email { get; set; } = email;
        
        public Guid Id { get; set; } = id;

        public IEnumerable<Guid>? GymLocationIds { get; set; } = gymLocationIds;
    }

    public class EditClientCommandHandler(
        IClientRepository clientRepository,
        IGymRepository gymRepository) : IRequestHandler<EditClientCommand, Result<Client>>
    {
        public async Task<Result<Client>> Handle(EditClientCommand request, CancellationToken cancellationToken)
        {
            Result<Client> existingResult = await clientRepository.GetClient(request.Id, cancellationToken);

            if (!existingResult.IsSuccess)
            {
                return existingResult;
            }

            Client client = existingResult.Value;
            client.Name = request.Name;
            client.Weight = request.Weight;
            client.Height = request.Height;
            client.Email = request.Email;

            Result<Client> clientResult = Validators.ClientValidator.Validate(client);

            if (!clientResult.IsSuccess)
            {
                return await Task.FromResult(clientResult);
            }

            if (request.GymLocationIds != null)
            {
                List<Guid> locationIds = request.GymLocationIds.Distinct().ToList();
                string? error = null;

                if (locationIds.Count > 0)
                {
                    if (client.GymId == null)
                    {
                        error = "Client that is not part of a gym cannot have gym locations.";
                    }
                    else
                    {
                        Result<Gym> gymResult = await gymRepository.GetGym(client.GymId.Value, cancellationToken);

                        if (!gymResult.IsSuccess)
                        {
                            error = gymResult.Message;
                        }
                        else if (locationIds.Any(locationId => !gymResult.Value.Locations.Any(l => l.Id == locationId)))
                        {
                            error = "All gym locations should belong to the client's gym.";
                        }
                    }
                }

                if (error != null)
                {
                    return new Result<Client>(value: client, isSuccess: false, message: error);
                }

                foreach (ClientGymLocation removed in client.ClientGymLocations
                    .Where(x => !locationIds.Contains(x.GymLocationId)).ToList())
                {
                    client.ClientGymLocations.Remove(removed);
                }

                foreach (Guid locationId in locationIds
                    .Where(locationId => !client.ClientGymLocations.Any(x => x.GymLocationId == locationId)))
                {
                    client.ClientGymLocations.Add(new ClientGymLocation()
                    {
                        ClientId = client.Id,
                        GymLocationId = locationId
                    });
                }
            }

            clientResult = await clientRepository.UpdateClient(client, cancellationToken);

            return clientResult;
        }
    }
}
