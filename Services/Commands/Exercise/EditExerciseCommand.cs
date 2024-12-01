namespace Services.Commands.Exercise
{
    using Entities.Common;
    using Entities.Exercise.Model;
    using MediatR;
    using Storage.Repositories.Exercise.Interface;
    using System.Threading;
    using System.Threading.Tasks;

    public class EditExerciseCommand(
        string name,
        string description,
        string videoUrl,
        Guid exerciseId) : IRequest<Result<Exercise>>
    {
        public string Name { get; } = name;

        public string Description { get; } = description;

        public string VideoUrl { get; } = videoUrl;

        public Guid ExerciseId { get; } = exerciseId;
    }

    public class EditExerciseCommandHandler(
        IExerciseRepository exerciseRepository) : IRequestHandler<EditExerciseCommand, Result<Exercise>>
    {
        public async Task<Result<Exercise>> Handle(EditExerciseCommand request, CancellationToken cancellationToken)
        {
            Result<Exercise> exerciseResult = await exerciseRepository.GetExercise(request.ExerciseId, cancellationToken);

            if (!exerciseResult.IsSuccess)
            {
                return exerciseResult;
            }

            exerciseResult.Value.VideoUrl = request.VideoUrl;
            exerciseResult.Value.Name = request.Name;
            exerciseResult.Value.Description = request.Description;

            Result<Exercise> result = await exerciseRepository.UpdateExercise(exerciseResult.Value, cancellationToken);

            return result;
        }
    }
}
