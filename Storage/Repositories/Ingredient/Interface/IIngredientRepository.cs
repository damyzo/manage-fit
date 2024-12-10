namespace Storage.Repositories.Ingredient.Interface
{
    using Entities.Common;
    using Entities.Ingredient.Model;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IIngredientRepository
    {
        public Task<Result<Ingredient>> GetIngredient(Guid ingredientId, CancellationToken cancellationToken);

        public Task<Result<IEnumerable<Ingredient>>> GetIngredients(Guid trainerId, CancellationToken cancellationToken);

        public Task<Result<Ingredient>> AddIngredient(Ingredient ingredient, CancellationToken cancellationToken);

        public Task<Result<Ingredient>> UpdateIngredient(Ingredient ingredient, CancellationToken cancellationToken);

        public Task<Result<Ingredient>> DeleteIngredient(Guid ingredientId, CancellationToken cancellationToken);
    }
}
