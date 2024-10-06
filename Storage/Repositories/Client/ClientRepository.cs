namespace Storage.Repositories.Client
{
    using Entities.Client.Model;
    using Microsoft.EntityFrameworkCore;
    using Services.Common;
    using Storage.DatabaseContext;
    using Storage.Repositories.Client.Interface;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class ClientRepository(ManageFitDbContext manageFitDbContext) : IClientRepository
    {
        public async Task<Result<Client>> GetClient(Guid clientUid)
        {
            Client? client = await manageFitDbContext.Client.Where(client => client.Uid == clientUid).FirstOrDefaultAsync();

            if (client == null)
            {
                Result<Client> clientError = new(
                    value: new Client { Name = "", Height = 0, Weight = 0, Email = "", Uid = Guid.Empty },
                    isSuccess: false,
                    message: "User Not Found");

                return clientError;
            }

            return new Result<Client>(
                value: client,
                isSuccess: true,
                message: "Valid Data");
        }

        public async Task<Result<IEnumerable<Client>>> GetClients()
        {
            IEnumerable<Client> clients = await manageFitDbContext.Client.ToListAsync();

            return new Result<IEnumerable<Client>>(value: clients, isSuccess: true, message: "Valid Data");
        }
    }
}
