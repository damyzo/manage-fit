namespace Storage.Repositories.Meal
{
    using Entities.Common;
    using Entities.Meal.Model;
    using Storage.Repositories.Meal.Interface;
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    public class MealRepository : IMealRepository
    {
        public Task<Result<Meal>> AddMeal(Meal meal, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<Result<Meal>> DeleteMeal(Guid mealId, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<Result<Meal>> GetMeal(Guid mealId, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<Result<IEnumerable<Meal>>> GetMeals(Guid trainerId, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<Result<Meal>> UpdateMeal(Meal meal, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
