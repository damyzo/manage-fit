namespace Services.Commands.Trainer
{
    using Entities.Common;
    using Entities.Gym.Model;
    using Entities.GymLocation.Model;
    using Entities.Trainer.Model;
    using MediatR;
    using Storage.Repositories.Trainer.Interface;
    using System.Threading;
    using System.Threading.Tasks;

    public class AddTrainerCommand(
        string name,
        string email,
        TrainerType type = TrainerType.Individual,
        string? gymName = null,
        IEnumerable<string>? gymLocations = null) : IRequest<Result<Trainer>>
    {
        public string Name { get; set; } = name;

        public string Email { get; set; } = email;

        public TrainerType Type { get; set; } = type;

        public string? GymName { get; set; } = gymName;

        public IEnumerable<string> GymLocations { get; set; } = gymLocations ?? [];
    }

    public class AddTrainerCommandHandler(
        ITrainerRepository trainerRepository) : IRequestHandler<AddTrainerCommand, Result<Trainer>>
    {
        public async Task<Result<Trainer>> Handle(AddTrainerCommand request, CancellationToken cancellationToken)
        {
            Result<Trainer> trainerByEmailResult = await trainerRepository.GetTrainerByEmail(request.Email, cancellationToken);

            if (trainerByEmailResult.IsSuccess)
            {
                return new Result<Trainer>(
                    value: new Trainer() { Email = request.Email, Name = "" },
                    isSuccess: false,
                    message: "This email is already in use.");
            }

            if (request.Type != TrainerType.Individual && request.Type != TrainerType.Owner)
            {
                return new Result<Trainer>(
                    value: new Trainer() { Email = request.Email, Name = "" },
                    isSuccess: false,
                    message: "Only Individual and Owner trainers can be created.");
            }

            Trainer trainer = new()
            {
                Name = request.Name,
                Email = request.Email,
                Type = request.Type,
                Id = Guid.NewGuid()
            };

            if (request.Type == TrainerType.Owner)
            {
                Gym gym = new()
                {
                    Name = request.GymName ?? string.Empty,
                    Id = Guid.NewGuid()
                };

                gym.Locations = request.GymLocations
                    .Select(x => new GymLocation { Id = Guid.NewGuid(), Address = x, GymId = gym.Id })
                    .ToList();

                Result<Gym> gymResult = Validators.GymValidator.Validate(gym);

                if (!gymResult.IsSuccess)
                {
                    return new Result<Trainer>(
                        value: new Trainer() { Email = request.Email, Name = "" },
                        isSuccess: false,
                        message: gymResult.Message);
                }

                trainer.Gym = gym;
                trainer.GymId = gym.Id;
            }
            else if (!string.IsNullOrEmpty(request.GymName) || request.GymLocations.Any())
            {
                return new Result<Trainer>(
                    value: new Trainer() { Email = request.Email, Name = "" },
                    isSuccess: false,
                    message: "Individual trainers cannot have a gym.");
            }

            Result<Trainer> trainerResult = await trainerRepository.AddTrainer(trainer, cancellationToken);

            if (!trainerResult.IsSuccess)
            {
                return trainerResult;
            }

            return trainerResult;
        }
    }
}
