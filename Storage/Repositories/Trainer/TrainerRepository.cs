namespace Storage.Repositories.Trainer
{
    using Storage.DatabaseContext;
    using Storage.Repositories.Trainer.Interface;

    public class TrainerRepository(ManageFitDbContext manageFitDbContext) : ITrainerRepository
    {
    }
}
