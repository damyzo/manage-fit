namespace Services.Commands.Gym
{
    using Entities.Common;
    using Entities.Gym.Model;
    using Entities.GymLocation.Model;
    using MediatR;
    using Storage.Repositories.Gym.Interface;

    public record EditGymLocation(Guid? Id, string Address);

    public class EditGymCommand(
        string name,
        IEnumerable<EditGymLocation> locations,
        Guid id) : IRequest<Result<Gym>>
    {
        public string Name { get; set; } = name;

        public IEnumerable<EditGymLocation> Locations { get; set; } = locations;

        public Guid Id { get; set; } = id;
    }

    public class EditGymCommandHandler(IGymRepository gymRepository) : IRequestHandler<EditGymCommand, Result<Gym>>
    {
        public async Task<Result<Gym>> Handle(EditGymCommand request, CancellationToken cancellationToken)
        {
            Gym gym = new()
            {
                Name = request.Name,
                Id = request.Id
            };

            gym.Locations = request.Locations
                .Select(x => new GymLocation { Id = x.Id ?? Guid.Empty, Address = x.Address, GymId = gym.Id })
                .ToList();

            Result<Gym> gymResult = Validators.GymValidator.Validate(gym);

            if (!gymResult.IsSuccess)
            {
                return gymResult;
            }

            return await gymRepository.UpdateGym(gym, cancellationToken);
        }
    }
}
