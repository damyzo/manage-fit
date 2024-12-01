namespace Services.Queries.Exercise
{
    using Entities.Common;
    using Entities.Exercise.Model;
    using MediatR;
    using Storage.Repositories.Exercise.Interface;
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    public class GetExerciseQuery(Guid exerciseId) : IRequest<Result<Exercise>>
    {
        public Guid ExerciseId { get; } = exerciseId;
    }

    public class GetExerciseQueryHandler(IExerciseRepository exerciseRepository) : IRequestHandler<GetExerciseQuery, Result<Exercise>>
    {
        public async Task<Result<Exercise>> Handle(GetExerciseQuery request, CancellationToken cancellationToken)
        {
            Result<Exercise> result = await exerciseRepository.GetExercise(request.ExerciseId, cancellationToken);

            return result;
        }
    }
}
