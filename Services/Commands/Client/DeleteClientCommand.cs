namespace Services.Commands.Client
{
    using Entities.Client.Model;
    using MediatR;
    using Entities.Common;
    using Storage.Repositories.Client.Interface;
    using System.Threading.Tasks;
    using System.Threading;

    public class DeleteClientCommand(Guid uid) : IRequest<Result<Client>>
    {
        public Guid Uid { get; set; } = uid;
    }

    public class DeleteClientCommandHandler(IClientRepository clientRepository) : IRequestHandler<DeleteClientCommand, Result<Client>>
    {
        public async Task<Result<Client>> Handle(DeleteClientCommand request, CancellationToken cancellationToken)
        {
            Result<Client> reuslt = await clientRepository.DeleteClient(request.Uid, cancellationToken);

            if (!reuslt.IsSuccess)
            {
                return reuslt;
            }
            
            return reuslt;
        }
    }
}
