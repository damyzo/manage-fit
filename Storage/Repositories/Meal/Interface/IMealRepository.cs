namespace Storage.Repositories.Meal.Interface
{
    using Entities.Common;
    using Entities.Meal.Model;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IMealRepository
    {
        public Task<Result<Meal>> GetMeal(Guid mealId, CancellationToken cancellationToken);

        public Task<Result<IEnumerable<Meal>>> GetMeals(Guid trainerId, CancellationToken cancellationToken);

        public Task<Result<Meal>> AddMeal(Meal meal, CancellationToken cancellationToken);

        public Task<Result<Meal>> UpdateMeal(Meal meal, CancellationToken cancellationToken);

        public Task<Result<Meal>> DeleteMeal(Guid mealId, CancellationToken cancellationToken);
    }
}
