namespace Storage.Repositories.Meal
{
    using Entities.Common;
    using Entities.Meal.Model;
    using Microsoft.EntityFrameworkCore;
    using Storage.DatabaseContext;
    using Storage.Repositories.Meal.Interface;
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    public class MealRepository(ManageFitDbContext manageFitDbContext) : IMealRepository
    {
        public async Task<Result<Meal>> AddMeal(Meal meal, CancellationToken cancellationToken)
        {
            manageFitDbContext.Meal.Add(meal);

            try
            {
                await manageFitDbContext.SaveChangesAsync(cancellationToken);
            }
            catch (Exception e)
            {
                Result<Meal> mealError = new(
                    value: new Meal { Id = Guid.Empty },
                    isSuccess: false,
                    message: e.Message);

                return mealError;
            }

            return new Result<Meal>(
                value: meal,
                isSuccess: true,
                message: "Valid Data");
        }

        public async Task<Result<Meal>> DeleteMeal(Guid mealId, CancellationToken cancellationToken)
        {
            Meal? meal = await manageFitDbContext.Meal.Where(meal => meal.Id == mealId).FirstOrDefaultAsync(cancellationToken);

            if (meal == null)
            {
                Result<Meal> mealError = new(
                    value: new Meal { Id = Guid.Empty },
                    isSuccess: false,
                    message: "Meal Not Found");

                return mealError;
            }

            manageFitDbContext.Meal.Remove(meal);

            try
            {
                await manageFitDbContext.SaveChangesAsync(cancellationToken);
            }
            catch (Exception e)
            {
                Result<Meal> mealError = new(
                    value: new Meal { Id = Guid.Empty },
                    isSuccess: false,
                    message: e.Message);

                return mealError;
            }

            return new Result<Meal>(
                value: meal,
                isSuccess: true,
                message: "Meal deleted");
        }

        public async Task<Result<Meal>> GetMeal(Guid mealId, CancellationToken cancellationToken)
        {
            Meal? meal = await manageFitDbContext.Meal.Where(meal => meal.Id == mealId).FirstOrDefaultAsync(cancellationToken);

            if (meal == null)
            {
                Result<Meal> mealError = new(
                    value: new Meal { Name = "", Id = Guid.Empty },
                    isSuccess: false,
                    message: "Meal Not Found");

                return mealError;
            }

            return new Result<Meal>(
                value: meal,
                isSuccess: true,
                message: "Valid Data");
        }

        public async Task<Result<IEnumerable<Meal>>> GetMeals(Guid trainerId, CancellationToken cancellationToken)
        {
            IEnumerable<Meal> meals = await manageFitDbContext.Meal
                .Where(meal => meal.TrainerId == trainerId)
                .ToListAsync(cancellationToken);

            return new Result<IEnumerable<Meal>>(value: meals, isSuccess: true, message: "Valid Data");
        }

        public async Task<Result<Meal>> UpdateMeal(Meal meal, CancellationToken cancellationToken)
        {
            manageFitDbContext.Meal.Update(meal);

            try
            {
                await manageFitDbContext.SaveChangesAsync(cancellationToken);
            }
            catch (Exception e)
            {
                Result<Meal> mealError = new(
                    value: new Meal { Name = "", Id = Guid.Empty },
                    isSuccess: false,
                    message: e.Message);

                return mealError;
            }

            return new Result<Meal>(
                value: meal,
                isSuccess: true,
                message: "Valid Data");
        }
    }
}
