﻿namespace Storage.Repositories.Client
{
    using Entities.Client.Model;
    using Entities.Common;
    using Microsoft.EntityFrameworkCore;
    using Storage.DatabaseContext;
    using Storage.Repositories.Client.Interface;
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    public class ClientRepository(ManageFitDbContext manageFitDbContext) : IClientRepository
    {
        public async Task<Result<Client>> AddClient(Client client, CancellationToken cancellationToken)
        {
            manageFitDbContext.Client.Add(client);

            try
            {
                await manageFitDbContext.SaveChangesAsync(cancellationToken);
            }
            catch (Exception e)
            {
                Result<Client> clientError = new(
                    value: new Client { Name = "", Height = 0, Weight = 0, Email = "", Uid = Guid.Empty },
                    isSuccess: false,
                    message: e.Message);

                return clientError;
            }

            return new Result<Client>(
                value: client,
                isSuccess: true,
                message: "Valid Data");
        }

        public async Task<Result<Client>> UpdateClient(Client client, CancellationToken cancellationToken)
        {
            manageFitDbContext.Client.Update(client);

            try
            {
                await manageFitDbContext.SaveChangesAsync(cancellationToken);
            }
            catch (Exception e)
            {
                Result<Client> clientError = new(
                    value: new Client { Name = "", Height = 0, Weight = 0, Email = "", Uid = Guid.Empty },
                    isSuccess: false,
                    message: e.Message);

                return clientError;
            }

            return new Result<Client>(
                value: client,
                isSuccess: true,
                message: "Valid Data");
        }

        public async Task<Result<Client>> GetClient(Guid clientUid, CancellationToken cancellationToken)
        {
            Client? client = await manageFitDbContext.Client.Where(client => client.Uid == clientUid).FirstOrDefaultAsync(cancellationToken);

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

        public async Task<Result<IEnumerable<Client>>> GetClients(Guid trainerGuid, CancellationToken cancellationToken)
        {
            IEnumerable<Client> clients = await manageFitDbContext.Client
                .Where(client => client.Trainers.Any(trainer => trainer.Uid == trainerGuid))
                .ToListAsync(cancellationToken);

            return new Result<IEnumerable<Client>>(value: clients, isSuccess: true, message: "Valid Data");
        }

        public async Task<Result<Client>> DeleteClient(Guid clientUid, CancellationToken cancellationToken)
        {
            Client? client = await manageFitDbContext.Client.Where(client => client.Uid == clientUid).FirstOrDefaultAsync(cancellationToken);

            if (client == null)
            {
                Result<Client> clientError = new(
                    value: new Client { Name = "", Height = 0, Weight = 0, Email = "", Uid = Guid.Empty },
                    isSuccess: false,
                    message: "User Not Found");

                return clientError;
            }

            manageFitDbContext.Client.Remove(client);

            try
            {
                await manageFitDbContext.SaveChangesAsync(cancellationToken);
            }
            catch (Exception e)
            {
                Result<Client> clientError = new(
                    value: new Client { Name = "", Height = 0, Weight = 0, Email = "", Uid = Guid.Empty },
                    isSuccess: false,
                    message: e.Message);

                return clientError;
            }

            return new Result<Client>(
                value: client,
                isSuccess: true,
                message: "Client deleted");
        }
    }
}
