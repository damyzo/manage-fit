namespace Storage.Repositories.Trainer.Interface
{
    using Entities.Common;
    using Entities.Trainer.Model;

    public interface ITrainerRepository
    {
        public Task<Result<Trainer>> GetTrainer(Guid trainerGuid, CancellationToken cancellationToken);
    }
}
