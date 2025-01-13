namespace Services.Commands.Trainer
{
    using Entities.Common;
    using Entities.Trainer.Model;
    using MediatR;
    using Storage.Repositories.Trainer.Interface;
    using System.Threading;
    using System.Threading.Tasks;

    public class AddTrainerCommand(string name, string email) : IRequest<Result<Trainer>>
    {
        public string Name { get; set; } = name;

        public string Email { get; set; } = email;
    }

    public class AddTrainerCommandHandler(
        ITrainerRepository trainerRepository) : IRequestHandler<AddTrainerCommand, Result<Trainer>>
    {
        public async Task<Result<Trainer>> Handle(AddTrainerCommand request, CancellationToken cancellationToken)
        {
            Trainer trainer = new()
            {
                Name = request.Name,
                Email = request.Email,
                Id = Guid.NewGuid()
            };
            
            Result<Trainer> trainerResult = await trainerRepository.AddTrainer(trainer, cancellationToken);

            if (!trainerResult.IsSuccess)
            {
                return trainerResult;
            }

            return trainerResult;
        }
    }
}
