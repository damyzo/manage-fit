namespace Storage.Repositories.Exercise
{
    using Entities.Common;
    using Entities.Exercise.Model;
    using Microsoft.EntityFrameworkCore;
    using Storage.DatabaseContext;
    using Storage.Repositories.Exercise.Interface;
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    public class ExerciseRepository(ManageFitDbContext manageFitDbContext) : IExerciseRepository
    {
        public async Task<Result<Exercise>> AddExercise(Exercise exercise, CancellationToken cancellationToken)
        {
            manageFitDbContext.Exercise.Add(exercise);

            try
            {
                await manageFitDbContext.SaveChangesAsync(cancellationToken);
            }
            catch (Exception e)
            {
                return new(
                    value: exercise,
                    isSuccess: false,
                    message: e.Message);
            }

            return new(
                    value: exercise,
                    isSuccess: true,
                    message: "Valid Data");
        }

        public async Task<Result<Exercise>> GetExercise(Guid exerciseId, CancellationToken cancellationToken)
        {
            Exercise? exercise = await manageFitDbContext.Exercise.Where(exercise => exercise.Id == exerciseId).FirstOrDefaultAsync(cancellationToken);

            if (exercise == null)
            {
                Result<Exercise> clientError = new(
                    value: new Exercise() { Name = "", Description = "", VideoUrl = "", Id = Guid.Empty },
                    isSuccess: false,
                    message: "Exercise Not Found");

                return clientError;
            }

            return new Result<Exercise>(
                value: exercise,
                isSuccess: true,
                message: "Valid Data");
        }
    }
}
