namespace Services.Commands.Gym
{
    using Entities.Common;
    using Entities.Gym.Model;
    using Entities.GymLocation.Model;
    using MediatR;
    using Storage.Repositories.Gym.Interface;

    public class AddGymCommand(
        string name,
        IEnumerable<string> locations) : IRequest<Result<Gym>>
    {
        public string Name { get; set; } = name;

        public IEnumerable<string> Locations { get; set; } = locations;
    }

    public class AddGymCommandHandler(IGymRepository gymRepository) : IRequestHandler<AddGymCommand, Result<Gym>>
    {
        public async Task<Result<Gym>> Handle(AddGymCommand request, CancellationToken cancellationToken)
        {
            Gym gym = new()
            {
                Name = request.Name,
                Id = Guid.NewGuid()
            };

            gym.Locations = request.Locations
                .Select(x => new GymLocation { Id = Guid.NewGuid(), Address = x, GymId = gym.Id })
                .ToList();

            Result<Gym> gymResult = Validators.GymValidator.Validate(gym);

            if (!gymResult.IsSuccess)
            {
                return gymResult;
            }

            return await gymRepository.AddGym(gym, cancellationToken);
        }
    }
}
