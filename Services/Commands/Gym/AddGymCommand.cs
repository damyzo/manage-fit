namespace Services.Commands.Gym
{
    using Entities.Common;
    using Entities.Gym.Model;
    using MediatR;
    using Storage.Repositories.Gym.Interface;

    public class AddGymCommand(
        string name,
        string location) : IRequest<Result<Gym>>
    {
        public string Name { get; set; } = name;

        public string Location { get; set; } = location;
    }

    public class AddGymCommandHandler(IGymRepository gymRepository) : IRequestHandler<AddGymCommand, Result<Gym>>
    {
        public async Task<Result<Gym>> Handle(AddGymCommand request, CancellationToken cancellationToken)
        {
            Gym gym = new()
            {
                Name = request.Name,
                Location = request.Location,
                Id = Guid.NewGuid()
            };

            Result<Gym> gymResult = Validators.GymValidator.Validate(gym);

            if (!gymResult.IsSuccess)
            {
                return gymResult;
            }

            return await gymRepository.AddGym(gym, cancellationToken);
        }
    }
}
