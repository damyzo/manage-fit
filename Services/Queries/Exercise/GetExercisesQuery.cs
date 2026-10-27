namespace Services.Queries.Exercise
{
    using Entities.Common;
    using Entities.Exercise.Model;
    using MediatR;
    using Storage.Repositories.Exercise.Interface;
    using Storage.Repositories.Trainer.Interface;
    using Entities.Trainer.Model;
    using System.Threading;
    using System.Threading.Tasks;

    public class GetExercisesQuery(string trainerEmail) : IRequest<Result<IEnumerable<Exercise>>>
    {
        public string TrainerEmail { get; } = trainerEmail;
    }

    public class GetExercisesQueryHandler(
        IExerciseRepository exerciseRepository,
        ITrainerRepository trainerRepository) : IRequestHandler<GetExercisesQuery, Result<IEnumerable<Exercise>>>
    {
        public async Task<Result<IEnumerable<Exercise>>> Handle(GetExercisesQuery request, CancellationToken cancellationToken)
        {
            Result<Trainer> trainerResult = await trainerRepository.GetTrainerByEmail(request.TrainerEmail, cancellationToken);

            if (!trainerResult.IsSuccess)
            {
                return new Result<IEnumerable<Exercise>>(value: [], isSuccess: false, message: trainerResult.Message);
            }

            Result<IEnumerable<Exercise>> result = await exerciseRepository.GetExercises(trainerResult.Value.Id, cancellationToken);

            return result;
        }
    }
}
