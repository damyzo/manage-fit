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

    public class GetClientQuery(Guid clientUid) : IRequest<Result<Client>>
    {
        public Guid ClientUid { get; set; } = clientUid;
    }
    public class GetClientQueryHandler(ManageFitDbContext manageFitDbContext) : IRequestHandler<GetClientQuery, Result<Client>>
    {
        public async Task<Result<Client>> Handle(GetClientQuery request, CancellationToken cancellationToken)
        {
            Client? client = await manageFitDbContext.Client.Where(client => client.Uid == request.ClientUid).FirstOrDefaultAsync();

            if (client == null)
            {
                Result<Client> clientError = new(
                    value: new Client { Name = "", Height = 0, Weight = 0, Email = "dasd", Uid = Guid.Empty },
                    isSuccess: false,
                    message: "User Not Found");
                
                return clientError;
            }
            
            return new Result<Client>(
                value: client,
                isSuccess: true,
                message: "Valid Data");
        }
    }
}
