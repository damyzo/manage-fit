namespace Storage.Repositories.Exercise.Interface
{
    using Entities.Common;
    using Entities.Exercise.Model;
    using System.Threading.Tasks;

    public interface IExerciseRepository
    {
        public Task<Result<Exercise>> GetExercise(Guid exerciseId, CancellationToken cancellationToken);

        public Task<Result<Exercise>> AddExercise(Exercise exercise, CancellationToken cancellationToken);
    }
}
