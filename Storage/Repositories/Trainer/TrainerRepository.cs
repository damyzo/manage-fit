﻿namespace Storage.Repositories.Trainer
{
    using Entities.Common;
    using Entities.Trainer.Model;
    using Microsoft.EntityFrameworkCore;
    using Storage.DatabaseContext;
    using Storage.Repositories.Trainer.Interface;
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    public class TrainerRepository(ManageFitDbContext manageFitDbContext) : ITrainerRepository
    {
        public async Task<Result<Trainer>> AddTrainer(Trainer trainer, CancellationToken cancellationToken)
        {
            manageFitDbContext.Trainer.Add(trainer);

            try
            {
                await manageFitDbContext.SaveChangesAsync();
            }
            catch (Exception e)
            {
                Result<Trainer> trainerResult =
                    new(
                        value: trainer,
                        isSuccess: false,
                        message: e.Message);
                
                return trainerResult;
            }

            return new Result<Trainer>(
                value: trainer,
                isSuccess: true,
                message: "Valid Data");
        }

        public async Task<Result<Trainer>> GetTrainer(Guid trainerId, CancellationToken cancellationToken)
        {
            Trainer? trainer = await manageFitDbContext
                .Trainer.Where(trainer => trainer.Id == trainerId)
                .FirstOrDefaultAsync(cancellationToken);

            if (trainer == null)
            {
                Result<Trainer> trainerError = new(
                    value: new Trainer { Name = "", Email = "", Id = Guid.Empty },
                    isSuccess: false,
                    message: "Trainer Not Found");

                return trainerError;
            }

            return new Result<Trainer>(
                value: trainer,
                isSuccess: true,
                message: "Valid Data");
        }

        public async Task<Result<Trainer>> GetTrainerByEmail(string email, CancellationToken cancellationToken)
        {
            Trainer? trainer = await manageFitDbContext
                .Trainer.Where(trainer => trainer.Email == email)
                .FirstOrDefaultAsync(cancellationToken);

            if (trainer == null)
            {
                Result<Trainer> trainerError = new(
                    value: new Trainer { Name = "", Email = "", Id = Guid.Empty },
                    isSuccess: false,
                    message: "Trainer Not Found");

                return trainerError;
            }

            return new Result<Trainer>(
                value: trainer,
                isSuccess: true,
                message: "Valid Data");
        }
    }
}
