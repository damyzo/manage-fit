namespace Storage.Repositories.Ingredient
{
    using Entities.Client.Model;
    using Entities.Common;
    using Entities.Ingredient.Model;
    using Microsoft.EntityFrameworkCore;
    using Storage.DatabaseContext;
    using Storage.Repositories.Ingredient.Interface;
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    public class IngredientRepository(ManageFitDbContext manageFitDbContext) : IIngredientRepository
    {
        public async Task<Result<Ingredient>> AddIngredient(Ingredient ingredient, CancellationToken cancellationToken)
        {
            manageFitDbContext.Ingredient.Add(ingredient);

            try
            {
                await manageFitDbContext.SaveChangesAsync(cancellationToken);
            }
            catch (Exception e)
            {
                Result<Ingredient> ingredientError = new(
                    value: new Ingredient { Id = Guid.Empty },
                    isSuccess: false,
                    message: e.Message);

                return ingredientError;
            }

            return new Result<Ingredient>(
                value: ingredient,
                isSuccess: true,
                message: "Valid Data");
        }

        public async Task<Result<Ingredient>> DeleteIngredient(Guid ingredientId, CancellationToken cancellationToken)
        {
            Ingredient? ingredient = await manageFitDbContext.Ingredient.Where(ingredient => ingredient.Id == ingredientId).FirstOrDefaultAsync(cancellationToken);

            if (ingredient == null)
            {
                Result<Ingredient> ingredientError = new(
                    value: new Ingredient { Name = "", Id = Guid.Empty },
                    isSuccess: false,
                    message: "Ingredient Not Found");

                return ingredientError;
            }

            manageFitDbContext.Ingredient.Remove(ingredient);

            try
            {
                await manageFitDbContext.SaveChangesAsync(cancellationToken);
            }
            catch (Exception e)
            {
                Result<Ingredient> ingredientError = new(
                    value: new Ingredient { Name = "", Id = Guid.Empty },
                    isSuccess: false,
                    message: e.Message);

                return ingredientError;
            }

            return new Result<Ingredient>(
                value: ingredient,
                isSuccess: true,
                message: "Client deleted");
        }

        public async Task<Result<Ingredient>> GetIngredient(Guid ingredientId, CancellationToken cancellationToken)
        {
            Ingredient? ingredient = await manageFitDbContext.Ingredient.Where(ingredient => ingredient.Id == ingredientId).FirstOrDefaultAsync(cancellationToken);

            if (ingredient == null)
            {
                Result<Ingredient> ingredientError = new(
                    value: new Ingredient { Name = "", Id = Guid.Empty },
                    isSuccess: false,
                    message: "User Not Found");

                return ingredientError;
            }

            return new Result<Ingredient>(
                value: ingredient,
                isSuccess: true,
                message: "Valid Data");
        }

        public async Task<Result<IEnumerable<Ingredient>>> GetIngredients(Guid trainerId, CancellationToken cancellationToken)
        {
            IEnumerable<Ingredient> ingredients = await manageFitDbContext.Ingredient
                .Where(ingredient => ingredient.TrainerId == trainerId)
                .ToListAsync(cancellationToken);

            return new Result<IEnumerable<Ingredient>>(value: ingredients, isSuccess: true, message: "Valid Data");
        }

        public async Task<Result<Ingredient>> UpdateIngredient(Ingredient ingredient, CancellationToken cancellationToken)
        {
            manageFitDbContext.Ingredient.Update(ingredient);

            try
            {
                await manageFitDbContext.SaveChangesAsync(cancellationToken);
            }
            catch (Exception e)
            {
                Result<Ingredient> ingredientError = new(
                    value: new Ingredient { Name = "", Id = Guid.Empty },
                    isSuccess: false,
                    message: e.Message);

                return ingredientError;
            }

            return new Result<Ingredient>(
                value: ingredient,
                isSuccess: true,
                message: "Valid Data");
        }
    }
}
