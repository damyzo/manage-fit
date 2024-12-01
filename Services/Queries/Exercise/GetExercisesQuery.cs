namespace Services.Queries.Exercise
{
    using Entities.Common;
    using Entities.Exercise.Model;
    using MediatR;
    using Storage.Repositories.Exercise.Interface;
    using System.Threading;
    using System.Threading.Tasks;

    public class GetExercisesQuery(Guid trainerId) : IRequest<Result<IEnumerable<Exercise>>>
    {
        public Guid TrainerId { get; } = trainerId;
    }

    public class GetExercisesQueryHandler(IExerciseRepository exerciseRepository) : IRequestHandler<GetExercisesQuery, Result<IEnumerable<Exercise>>>
    {
        public async Task<Result<IEnumerable<Exercise>>> Handle(GetExercisesQuery request, CancellationToken cancellationToken)
        {
            Result<IEnumerable<Exercise>> result = await exerciseRepository.GetExercises(request.TrainerId, cancellationToken);

            return result;
        }
    }
}
