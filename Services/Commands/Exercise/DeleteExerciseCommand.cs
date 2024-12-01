namespace Services.Commands.Exercise
{
    using Entities.Common;
    using Entities.Exercise.Model;
    using MediatR;
    using Storage.Repositories.Exercise.Interface;
    using System.Threading;
    using System.Threading.Tasks;

    public class DeleteExerciseCommand(Guid exerciseId) : IRequest<Result<Exercise>>
    {
        public Guid ExerciseId { get; } = exerciseId;
    }

    public class DeleteExerciseCommandHandler(IExerciseRepository exerciseRepository) : IRequestHandler<DeleteExerciseCommand, Result<Exercise>>
    {
        public async Task<Result<Exercise>> Handle(DeleteExerciseCommand request, CancellationToken cancellationToken)
        {
            Result<Exercise> result = await exerciseRepository.DeleteExercise(request.ExerciseId, cancellationToken);

            return result;
        }
    }
}
