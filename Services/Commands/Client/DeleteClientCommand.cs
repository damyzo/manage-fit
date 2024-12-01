namespace Services.Commands.Client
{
    using Entities.Client.Model;
    using MediatR;
    using Entities.Common;
    using Storage.Repositories.Client.Interface;
    using System.Threading.Tasks;
    using System.Threading;

    public class DeleteClientCommand(Guid id) : IRequest<Result<Client>>
    {
        public Guid Id { get; set; } = id;
    }

    public class DeleteClientCommandHandler(IClientRepository clientRepository) : IRequestHandler<DeleteClientCommand, Result<Client>>
    {
        public async Task<Result<Client>> Handle(DeleteClientCommand request, CancellationToken cancellationToken)
        {
            Result<Client> reuslt = await clientRepository.DeleteClient(request.Id, cancellationToken);

            return reuslt;
        }
    }
}
