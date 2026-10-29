namespace Storage.Repositories.Gym.Interface
{
    using Entities.Common;
    using Entities.Gym.Model;

    public interface IGymRepository
    {
        public Task<Result<Gym>> GetGym(Guid gymId, CancellationToken cancellationToken);

        public Task<Result<IEnumerable<Gym>>> GetGyms(CancellationToken cancellationToken);

        public Task<Result<Gym>> AddGym(Gym gym, CancellationToken cancellationToken);

        public Task<Result<Gym>> UpdateGym(Gym gym, CancellationToken cancellationToken);

        public Task<Result<Gym>> DeleteGym(Guid gymId, CancellationToken cancellationToken);
    }
}
