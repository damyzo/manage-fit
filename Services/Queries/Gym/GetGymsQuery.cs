namespace Services.Queries.Gym
{
    using Entities.Common;
    using Entities.Gym.Model;
    using MediatR;
    using Storage.Repositories.Gym.Interface;

    public class GetGymsQuery : IRequest<Result<IEnumerable<Gym>>>
    {
    }

    public class GetGymsQueryHandler(IGymRepository gymRepository) : IRequestHandler<GetGymsQuery, Result<IEnumerable<Gym>>>
    {
        public async Task<Result<IEnumerable<Gym>>> Handle(GetGymsQuery request, CancellationToken cancellationToken)
        {
            return await gymRepository.GetGyms(cancellationToken);
        }
    }
}
