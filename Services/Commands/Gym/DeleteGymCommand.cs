namespace Services.Commands.Gym
{
    using Entities.Common;
    using Entities.Gym.Model;
    using MediatR;
    using Storage.Repositories.Gym.Interface;

    public class DeleteGymCommand(Guid id) : IRequest<Result<Gym>>
    {
        public Guid Id { get; set; } = id;
    }

    public class DeleteGymCommandHandler(IGymRepository gymRepository) : IRequestHandler<DeleteGymCommand, Result<Gym>>
    {
        public async Task<Result<Gym>> Handle(DeleteGymCommand request, CancellationToken cancellationToken)
        {
            return await gymRepository.DeleteGym(request.Id, cancellationToken);
        }
    }
}
