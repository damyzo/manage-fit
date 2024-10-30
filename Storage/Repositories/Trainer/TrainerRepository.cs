namespace Storage.Repositories.Trainer
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
        public async Task<Result<Trainer>> GetTrainer(Guid trainerGuid, CancellationToken cancellationToken)
        {
            Trainer? trainer = await manageFitDbContext
                .Trainer.Where(trainer => trainer.Uid == trainerGuid)
                .FirstOrDefaultAsync(cancellationToken);

            if (trainer == null)
            {
                Result<Trainer> trainerError = new(
                    value: new Trainer { Name = "", Email = "", Uid = Guid.Empty },
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
