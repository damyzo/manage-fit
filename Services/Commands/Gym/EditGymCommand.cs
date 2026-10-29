namespace Services.Commands.Gym
{
    using Entities.Common;
    using Entities.Gym.Model;
    using MediatR;
    using Storage.Repositories.Gym.Interface;

    public class EditGymCommand(
        string name,
        string location,
        Guid id) : IRequest<Result<Gym>>
    {
        public string Name { get; set; } = name;

        public string Location { get; set; } = location;

        public Guid Id { get; set; } = id;
    }

    public class EditGymCommandHandler(IGymRepository gymRepository) : IRequestHandler<EditGymCommand, Result<Gym>>
    {
        public async Task<Result<Gym>> Handle(EditGymCommand request, CancellationToken cancellationToken)
        {
            Gym gym = new()
            {
                Name = request.Name,
                Location = request.Location,
                Id = request.Id
            };

            Result<Gym> gymResult = Validators.GymValidator.Validate(gym);

            if (!gymResult.IsSuccess)
            {
                return gymResult;
            }

            return await gymRepository.UpdateGym(gym, cancellationToken);
        }
    }
}
