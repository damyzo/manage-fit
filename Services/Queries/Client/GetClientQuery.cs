namespace Services.Queries.Client
{
    using MediatR;
    using Services.Common;
    using System;
    using Entities.Client.Model;
    using System.Threading.Tasks;
    using System.Threading;
    using Storage.DatabaseContext;
    using Microsoft.EntityFrameworkCore;
    using Storage.Repositories.Client.Interface;

    public class GetClientQuery(Guid clientUid) : IRequest<Result<Client>>
    {
        public Guid ClientUid { get; set; } = clientUid;
    }
    public class GetClientQueryHandler(IClientRepository clientRepository) : IRequestHandler<GetClientQuery, Result<Client>>
    {
        public async Task<Result<Client>> Handle(GetClientQuery request, CancellationToken cancellationToken)
        {
            return await clientRepository.GetClient(request.ClientUid);
        }
    }
}
