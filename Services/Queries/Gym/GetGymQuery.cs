namespace Services.Queries.Gym
{
    using Entities.Common;
    using Entities.Gym.Model;
    using MediatR;
    using Storage.Repositories.Gym.Interface;

    public class GetGymQuery(Guid gymId) : IRequest<Result<Gym>>
    {
        public Guid GymId { get; set; } = gymId;
    }

    public class GetGymQueryHandler(IGymRepository gymRepository) : IRequestHandler<GetGymQuery, Result<Gym>>
    {
        public async Task<Result<Gym>> Handle(GetGymQuery request, CancellationToken cancellationToken)
        {
            return await gymRepository.GetGym(request.GymId, cancellationToken);
        }
    }
}
