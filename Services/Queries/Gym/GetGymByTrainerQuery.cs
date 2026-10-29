namespace Services.Queries.Gym
{
    using Entities.Common;
    using Entities.Gym.Model;
    using Entities.Trainer.Model;
    using MediatR;
    using Storage.Repositories.Gym.Interface;
    using Storage.Repositories.Trainer.Interface;

    public class GetGymByTrainerQuery(string trainerEmail) : IRequest<Result<Gym>>
    {
        public string TrainerEmail { get; } = trainerEmail;
    }

    public class GetGymByTrainerQueryHandler(
        IGymRepository gymRepository,
        ITrainerRepository trainerRepository) : IRequestHandler<GetGymByTrainerQuery, Result<Gym>>
    {
        public async Task<Result<Gym>> Handle(GetGymByTrainerQuery request, CancellationToken cancellationToken)
        {
            Result<Trainer> trainerResult = await trainerRepository.GetTrainerByEmail(request.TrainerEmail, cancellationToken);

            if (!trainerResult.IsSuccess)
            {
                return new Result<Gym>(
                    value: new Gym { Name = "", Id = Guid.Empty },
                    isSuccess: false,
                    message: trainerResult.Message);
            }

            if (trainerResult.Value.GymId == null)
            {
                return new Result<Gym>(
                    value: new Gym { Name = "", Id = Guid.Empty },
                    isSuccess: false,
                    message: "Trainer is not part of a gym.");
            }

            return await gymRepository.GetGym(trainerResult.Value.GymId.Value, cancellationToken);
        }
    }
}
