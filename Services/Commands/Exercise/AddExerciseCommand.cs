namespace Services.Commands.Exercise
{
    using Entities.Common;
    using Entities.Exercise.Model;
    using Entities.Trainer.Model;
    using MediatR;
    using Storage.Repositories.Exercise.Interface;
    using Storage.Repositories.Trainer.Interface;
    using System.Threading;
    using System.Threading.Tasks;

    public class AddExerciseCommand(
        string name,
        string description,
        string videoUrl,
        string trainerEmail) : IRequest<Result<Exercise>>
    {
        public string Name { get; } = name;

        public string Description { get; } = description;

        public string VideoUrl { get; } = videoUrl;

        public string TrainerEmail { get; } = trainerEmail;
    }

    public class AddExerciseCommandHandler(
        IExerciseRepository exerciseRepository,
        ITrainerRepository trainerRepository) : IRequestHandler<AddExerciseCommand, Result<Exercise>>
    {
        public async Task<Result<Exercise>> Handle(AddExerciseCommand request, CancellationToken cancellationToken)
        {
            Result<Trainer> trainerResult = await trainerRepository.GetTrainerByEmail(request.TrainerEmail, cancellationToken);

            if (!trainerResult.IsSuccess)
            {
                return new Result<Exercise>(
                    value: new Exercise()
                    {
                        Name = request.Name,
                        Description = request.Description,
                        VideoUrl = request.VideoUrl
                    },
                    isSuccess: trainerResult.IsSuccess,
                    message: trainerResult.Message);
            }

            Exercise exercise = new()
            {
                Description = request.Description,
                Name = request.Name,
                VideoUrl = request.VideoUrl,
                TrainerId = trainerResult.Value.Id,
                Id = Guid.NewGuid()
            };

            Result<Exercise> exerciseResult = await exerciseRepository.AddExercise(exercise, cancellationToken);

            return exerciseResult;
        }
    }
}
