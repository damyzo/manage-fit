
namespace Services.Queries.Client
{
    using Entities.Client.Model;
    using Entities.Common;
    using MediatR;
    using Storage.Repositories.Client.Interface;
    using System.Collections.Generic;

    using System.Threading;
    using System.Threading.Tasks;

    public class GetClientsQuery(Guid trainerId) : IRequest<Result<IEnumerable<Client>>> 
    {
        public Guid TrainerId { get; } = trainerId;
    }

    public class GetClientsQueryHandler(IClientRepository clientRepository) : IRequestHandler<GetClientsQuery, Result<IEnumerable<Client>>>
    {
        public async Task<Result<IEnumerable<Client>>> Handle(GetClientsQuery request, CancellationToken cancellationToken)
        {
            return await clientRepository.GetClients(request.TrainerId, cancellationToken);
        }
    }

}
