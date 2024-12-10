namespace Storage.Repositories.Ingredient
{
    using Entities.Common;
    using Entities.Ingredient.Model;
    using Storage.Repositories.Ingredient.Interface;
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    public class IngredientRepository : IIngredientRepository
    {
        public Task<Result<Ingredient>> AddIngredient(Ingredient ingredient, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<Result<Ingredient>> DeleteIngredient(Guid ingredientId, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<Result<Ingredient>> GetIngredient(Guid ingredientId, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<Result<IEnumerable<Ingredient>>> GetIngredients(Guid trainerId, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<Result<Ingredient>> UpdateIngredient(Ingredient ingredient, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
