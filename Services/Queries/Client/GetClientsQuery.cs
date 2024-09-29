
namespace Services.Queries.Client
{
    using Entities.Client.Model;
    using MediatR;
    using Microsoft.EntityFrameworkCore;
    using Services.Common;
    using Storage.DatabaseContext;
    using System.Collections.Generic;

    using System.Threading;
    using System.Threading.Tasks;

    public class GetClientsQuery() : IRequest<Result<IEnumerable<Client>>> {}

    public class GetClientsQueryHandler(ManageFitDbContext manageFitDbContext) : IRequestHandler<GetClientsQuery, Result<IEnumerable<Client>>>
    {
        public async Task<Result<IEnumerable<Client>>> Handle(GetClientsQuery request, CancellationToken cancellationToken)
        {
            IEnumerable<Client> clients = await manageFitDbContext.Client.ToListAsync(cancellationToken);

            return new Result<IEnumerable<Client>>(value: clients, isSuccess: true, message: "Valid Data");
        }
    }

}
