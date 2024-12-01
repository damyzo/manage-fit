namespace Storage.Repositories.Exercise
{
    using Entities.Common;
    using Entities.Exercise.Model;
    using Microsoft.EntityFrameworkCore;
    using Storage.DatabaseContext;
    using Storage.Repositories.Exercise.Interface;
    using System;
    using System.Collections.Generic;
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

        public async Task<Result<Exercise>> DeleteExercise(Guid exerciseId, CancellationToken cancellationToken)
        {
            Exercise? exercise = await manageFitDbContext.Exercise.Where(exercise => exercise.Id == exerciseId).FirstOrDefaultAsync(cancellationToken);

            if (exercise == null)
            {
                Result<Exercise> exerciseError = new(
                    value: new Exercise { Name = "", Description = "", VideoUrl = "", Id = Guid.Empty },
                    isSuccess: false,
                    message: "User Not Found");

                return exerciseError;
            }

            manageFitDbContext.Exercise.Remove(exercise);

            try
            {
                await manageFitDbContext.SaveChangesAsync(cancellationToken);
            }
            catch (Exception e)
            {
                Result<Exercise> exerciseError = new(
                    value: new Exercise { Name = "", Description = "", VideoUrl = "", Id = Guid.Empty },
                    isSuccess: false,
                    message: e.Message);

                return exerciseError;
            }

            return new Result<Exercise>(
                value: exercise,
                isSuccess: true,
                message: "Client deleted");
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

        public async Task<Result<IEnumerable<Exercise>>> GetExercises(Guid trainerId, CancellationToken cancellationToken)
        {
            IEnumerable<Exercise> exercises = await manageFitDbContext.Exercise
                .Where(exercises => exercises.Trainer.Id == trainerId)
                .ToListAsync(cancellationToken);

            return new Result<IEnumerable<Exercise>>(value: exercises, isSuccess: true, message: "Valid Data");
        }

        public async Task<Result<Exercise>> UpdateExercise(Exercise exercise, CancellationToken cancellationToken)
        {
            manageFitDbContext.Exercise.Update(exercise);

            try
            {
                await manageFitDbContext.SaveChangesAsync(cancellationToken);
            }
            catch (Exception e)
            {
                Result<Exercise> exerciseError = new(
                    value: new Exercise { Name = "", Description = "", VideoUrl = "", Id = Guid.Empty },
                    isSuccess: false,
                    message: e.Message);

                return exerciseError;
            }

            return new Result<Exercise>(
                value: exercise,
                isSuccess: true,
                message: "Valid Data");
        }
    }
}
